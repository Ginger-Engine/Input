using System.Drawing;
using Engine.Core;
using Engine.Core.Behaviours;
using Engine.Core.Entities;
using Engine.Core.Scenes;
using Engine.Core.Transform;
using Engine.Rendering;

namespace Engine.Input.PointerEvents;

public interface IPointerEvent { }

public struct EnterEvent : IPointerEvent { }
public struct LeaveEvent : IPointerEvent { }

public class PointerInputManager : IEntityBehaviour
{
    private readonly IPointerProvider _pointerProvider;

    // Подписки на события
    private readonly Dictionary<Type, Dictionary<Entity, Action<Entity>>> _subscriptions = new();

    // Текущее состояние наведения
    private readonly Dictionary<Entity, bool> _hoverStates = new();

    public PointerInputManager(IPointerProvider pointerProvider)
    {
        _pointerProvider = pointerProvider;
    }

    public void Subscribe<TEvent>(Entity entity, Action<Entity> callback)
    {
        var type = typeof(TEvent);
        if (!_subscriptions.TryGetValue(type, out var entityCallbacks))
            _subscriptions[type] = entityCallbacks = new();

        entityCallbacks[entity] = callback;

        // Подписка на Enter/Leave добавляет слежение за hovered
        if (type == typeof(EnterEvent) || type == typeof(LeaveEvent))
            _hoverStates.TryAdd(entity, false);
    }

    public void Unsubscribe<TEvent>(Entity entity)
    {
        var type = typeof(TEvent);
        if (_subscriptions.TryGetValue(type, out var entityCallbacks))
        {
            entityCallbacks.Remove(entity);

            // Если больше нет подписок на Enter/Leave — удалить отслеживание
            if ((type == typeof(EnterEvent) || type == typeof(LeaveEvent)) &&
                !_subscriptions.TryGetValue(typeof(EnterEvent), out var enterSubsOrEmpty) &&
                !_subscriptions.TryGetValue(typeof(LeaveEvent), out var leaveSubsOrEmpty))
            {
                _hoverStates.Remove(entity);
            }
        }
    }

    public void OnUpdate(Entity _, float dt)
    {
        var pointer = _pointerProvider.GetPointerPosition();

        // Обновляем только тех, кто имеет хотя бы подписку на Enter/Leave
        foreach (var (entity, wasHovered) in _hoverStates.ToArray())
        {
            if (!entity.IsEnabled) continue;

            if (!entity.TryGetComponent<TransformComponent>(out var tr))
                throw new GingerException($"Entity '{entity}' is subscribed to pointer input but has no TransformComponent.");

            if (!entity.TryGetComponent<RectangleComponent>(out var rect))
                throw new GingerException($"Entity '{entity}' is subscribed to pointer input but has no RectangleComponent.");

            var pos = tr.WorldTransform.Position;
            var size = rect.SizeCache;
            var bounds = new RectangleF(pos.X, pos.Y, size.X, size.Y);
            var isHovered = bounds.Contains(pointer.X, pointer.Y);

            if (isHovered && !wasHovered)
                Invoke<EnterEvent>(entity);
            else if (!isHovered && wasHovered)
                Invoke<LeaveEvent>(entity);

            _hoverStates[entity] = isHovered;
        }
    }

    private void Invoke<TEvent>(Entity entity)
    {
        if (_subscriptions.TryGetValue(typeof(TEvent), out var handlers) &&
            handlers.TryGetValue(entity, out var callback))
        {
            callback(entity);
        }
    }
}
