using Microsoft.Practices.Unity;

namespace ToDoAndDiary
{

    public class IoCContainer
    {
        private static UnityContainer _container;

        private IoCContainer() { }

        public static UnityContainer Instance
        {
            get
            {
                if (_container == null)
                    _container = new UnityContainer();
                return _container;
            }
        }

    }

}