using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnxConsole.Domain.Events
{
	public static class DomainEvents
	{
	    [ThreadStatic] //so that each thread has its own callbacks don't do this from aspnet thread
	    private static List<Delegate> _actions;// = new List<delegate>();
        [ThreadStatic]
        private static List<Func<IDomainEvent,bool>> _funcs;

        // Registers a callback for the given domain event
        public static void SubscribeTo<T>(Action<T> callback)  where T : IDomainEvent
		{
			if(_actions == null) _actions = new List<Delegate>();
			
			_actions.Add(callback);
		}

        public static void SubscribeTo(Func<IDomainEvent, bool> callback)
        {
            if (_funcs == null) _funcs = new List<Func<IDomainEvent,bool>>();

            _funcs.Add(callback);
        }


        //Clears callbacks passed to Register on the current thread
        public static void ClearCallbacks ()
       {
           _actions = null;
           _funcs = null;
       }
	   
	   //Raises the given domain event
       public static void Publish<T>(T args) where T : IDomainEvent
       {
            if (_actions == null) return;
            foreach (var a in _actions.Select(action => action as Action<T>))
            {
                a?.Invoke(args);
            }
        }

        public static async Task<bool> PublishAsync(IDomainEvent args)
        {
            // if (_actions == null) return false;
            var funcs = _funcs
                .Select(action => action)
                .Where(a => a != null)
                .Select(Asyncify);

            await Task.WhenAll(funcs);
      
            //return Task.FromResult<bool>(true);
            return true;
        }


        private static Task<Func<IDomainEvent, bool>> Asyncify(Func<IDomainEvent, bool> func)
	    {
	        return Task.FromResult(func);
	    }
	}
}