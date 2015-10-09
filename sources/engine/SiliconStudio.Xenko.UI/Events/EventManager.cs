﻿// Copyright (c) 2014 Silicon Studio Corp. (http://siliconstudio.co.jp)
// This file is distributed under GPL v3. See LICENSE.md for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SiliconStudio.Xenko.UI.Events
{
    /// <summary>
    /// Provides event-related utility methods that register routed events for class owners and add class handlers.
    /// </summary>
    public static class EventManager
    {
        /// <summary>
        /// Finds the routed event identified by its name and owner.
        /// </summary>
        /// <param name="ownerType">The type to start the search with. Base classes are included in the search.</param>
        /// <param name="eventName">The event name.</param>
        /// <returns>The matching routed event identifier if any match is found; otherwise, null.</returns>
        public static RoutedEvent GetRoutedEvent(Type ownerType, string eventName)
        {
            var currentType = ownerType;
            while (currentType != null)
            {
                if (ownerToEvents.ContainsKey(currentType) && ownerToEvents[currentType].ContainsKey(eventName))
                    return ownerToEvents[currentType][eventName];

                currentType = currentType.GetTypeInfo().BaseType;
            }

            return null;
        }

        /// <summary>
        /// Returns identifiers for routed events that have been registered to the event system.
        /// </summary>
        /// <returns>An array of type <see cref="RoutedEvent"/> that contains the registered objects.</returns>
        public static RoutedEvent[] GetRoutedEvents()
        {
            return routedEvents.ToArray();
        }

        /// <summary>
        /// Finds all routed event identifiers for events that are registered with the provided owner type.
        /// </summary>
        /// <param name="ownerType">The type to start the search with. Base classes are included in the search.</param>
        /// <returns>An array of matching routed event identifiers if any match is found; otherwise, null.</returns>
        public static RoutedEvent[] GetRoutedEventsForOwner(Type ownerType)
        {
            var types = new List<Type>();
                
            var currentType = ownerType;
            while (currentType != null)
            {
                types.Add(currentType);
                currentType = currentType.GetTypeInfo().BaseType;
            }

            return types.Where(t => ownerToEvents.ContainsKey(t)).SelectMany(t => ownerToEvents[t].Values).ToArray();
        }

        /// <summary>
        /// Registers a class handler for a particular routed event, with the option to handle events where event data is already marked handled.
        /// </summary>
        /// <param name="classType">The type of the class that is declaring class handling.</param>
        /// <param name="routedEvent">The routed event identifier of the event to handle.</param>
        /// <param name="handler">A reference to the class handler implementation.</param>
        /// <param name="handledEventsToo">true to invoke this class handler even if arguments of the routed event have been marked as handled; 
        /// false to retain the default behavior of not invoking the handler on any marked-handled event.</param>
        /// <exception cref="ArgumentNullException"><paramref name="classType"/>, <paramref name="routedEvent"/>, or <paramref name="handler"/> is null.</exception>
        public static void RegisterClassHandler<T>(Type classType, RoutedEvent<T> routedEvent, EventHandler<T> handler, bool handledEventsToo = false) where T : RoutedEventArgs
        {
            if (classType == null) throw new ArgumentNullException("classType");
            if (routedEvent == null) throw new ArgumentNullException("routedEvent");
            if (handler == null) throw new ArgumentNullException("handler");

            if(!classesToClassHandlers.ContainsKey(classType))
                classesToClassHandlers[classType] = new Dictionary<RoutedEvent, RoutedEventHandlerInfo>();

            classesToClassHandlers[classType][routedEvent] = new RoutedEventHandlerInfo<T>(handler, handledEventsToo);
        }

        /// <summary>
        /// Get the class handler for the class <paramref name="classType"/> and routed event <paramref name="routedEvent"/>.
        /// </summary>
        /// <param name="classType">The type of the class that is handling the event.</param>
        /// <param name="routedEvent">The routed event to handle</param>
        /// <returns>The class handler</returns>
        /// <exception cref="ArgumentNullException"><paramref name="classType"/>, or <paramref name="routedEvent"/> is null.</exception>
        internal static RoutedEventHandlerInfo GetClassHandler(Type classType, RoutedEvent routedEvent)
        {
            if (classType == null) throw new ArgumentNullException("classType");
            if (routedEvent == null) throw new ArgumentNullException("routedEvent");

            var currentType = classType;
            while (currentType != null)
            {
                if (classesToClassHandlers.ContainsKey(currentType) && classesToClassHandlers[currentType].ContainsKey(routedEvent))
                    return classesToClassHandlers[currentType][routedEvent];

                currentType = currentType.GetTypeInfo().BaseType;
            }

            return null;
        }

        private readonly static Dictionary<Type, Dictionary<RoutedEvent, RoutedEventHandlerInfo>> classesToClassHandlers = new Dictionary<Type, Dictionary<RoutedEvent, RoutedEventHandlerInfo>>();

        /// <summary>
        /// Registers a new routed event.
        /// </summary>
        /// <param name="name">The name of the routed event. The name must be unique within the owner type (base class included) and cannot be null or an empty string.</param>
        /// <param name="routingStrategy">The routing strategy of the event as a value of the enumeration.</param>
        /// <param name="ownerType">The owner class type of the routed event. This cannot be null.</param>
        /// <returns>The identifier for the newly registered routed event. 
        /// This identifier object can now be stored as a static field in a class and then used as a parameter for methods that attach handlers to the event. 
        /// The routed event identifier is also used for other event system APIs.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> or <paramref name="ownerType"/> is null.</exception>
        /// <exception cref="InvalidOperationException">This exception is thrown if a routed event of name <paramref name="name"/> already exists for type <paramref name="ownerType"/> and parents.
        /// </exception>
        public static RoutedEvent<T> RegisterRoutedEvent<T>(string name, RoutingStrategy routingStrategy, Type ownerType) where T: RoutedEventArgs
        {
            if (name == null) throw new ArgumentNullException("name");
            if (ownerType == null) throw new ArgumentNullException("ownerType");
            
            if (GetRoutedEvent(ownerType, name) != null)
                throw new InvalidOperationException("A routed event named '" + name + "' already exists in provided owner type '" + ownerType + "' or base classes.");

            var newRoutedEvent = new RoutedEvent<T> {  Name = name, OwnerType = ownerType, RoutingStrategy = routingStrategy, };
            routedEvents.Add(newRoutedEvent);
            
            if(!ownerToEvents.ContainsKey(ownerType))
                ownerToEvents[ownerType] = new Dictionary<string, RoutedEvent>();

            ownerToEvents[ownerType][name] = newRoutedEvent;

            return newRoutedEvent;
        }

        private readonly static List<RoutedEvent> routedEvents = new List<RoutedEvent>();
        private readonly static Dictionary<Type, Dictionary<string, RoutedEvent>> ownerToEvents = new Dictionary<Type, Dictionary<string, RoutedEvent>>();
 
        /// <summary>
        /// This functions reset all the registers and invalidate all the created routed events.
        /// It is mostly used for tests purposes.
        /// </summary>
        internal static void ResetRegisters()
        {
            routedEvents.Clear();
            ownerToEvents.Clear();
            classesToClassHandlers.Clear();
        }
    }
}