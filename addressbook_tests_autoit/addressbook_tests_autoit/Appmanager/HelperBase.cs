using AutoItX3Lib;

namespace addressbook_tests_autoit
{
    public class HelperBase
    {
        protected ApplicationManager manager;
        protected AutoItX3 aux;

        protected string WINTITLE;

        public HelperBase(ApplicationManager manager)
        {
            this.manager = manager;
            aux = manager.Aux;

            WINTITLE = ApplicationManager.WINTITLE;
        }
    }
}