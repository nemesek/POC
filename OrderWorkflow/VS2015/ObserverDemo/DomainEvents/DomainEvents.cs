using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// from http://www.udidahan.com/2009/06/14/domain-events-salvation/
namespace DomainEvents
{
	public static class DomainEvents
	{
	    [ThreadStatic] //so that each thread has its own callbacks
	    private static List<Delegate> _actions;// = new List<delegate>();

	   // private static List<Func<IDomainEvent, bool> _funcs;
		
		//public static IContainer Container {get; set;}
		
		// Registers a callback for the given domain event
		public static void Register<T>(Action<T> callback)  where T : IDomainEvent
		{
			if(_actions == null) _actions = new List<Delegate>();
			
			_actions.Add(callback);
		}

        public static void Register<T>(Func<T, bool> callback) where T : IDomainEvent
        {
            if (_actions == null) _actions = new List<Delegate>();

            _actions.Add(callback);
        }

        //Clears callbacks passed to Register on the current thread
        public static void ClearCallbacks ()
       {
           _actions = null;
       }
	   
	   //Raises the given domain event
       public static void Raise<T>(T args) where T : IDomainEvent
       {
            //           if (Container != null)
            // 		  {
            //            	foreach(var handler in Container.ResolveAll<Handles<T>>())
            //                 {
            // 					handler.Handle(args);
            // 				}
            // 		  }


            if (_actions == null) return;
            foreach (var a in _actions.Select(action => action as Action<T>))
            {
                a?.Invoke(args);
                
            }
        }

	    //public static async Task<bool> RaiseAsync<T>(T args) where T : IDomainEvent
	    //{
     //       if (_actions == null) return false;
     //       foreach (var a in _actions.Select(action => action as Action<T>))
     //       {
     //           if (a != null)
     //           {
     //               a.Invoke(default(T));
     //           }
     //       }
     //       //return Task.FromResult<bool>(true);
     //       return true;
	    //}
    }
}