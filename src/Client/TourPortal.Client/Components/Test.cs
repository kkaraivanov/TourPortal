namespace TourPortal.Client.Components
{
    using System.ComponentModel.Design;

    public class Test : IMenuCommandService
    {
        public void AddCommand(MenuCommand command)
        {
            throw new System.NotImplementedException();
        }

        public void AddVerb(DesignerVerb verb)
        {
            throw new System.NotImplementedException();
        }

        public MenuCommand FindCommand(CommandID commandID)
        {
            throw new System.NotImplementedException();
        }

        public bool GlobalInvoke(CommandID commandID)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveCommand(MenuCommand command)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveVerb(DesignerVerb verb)
        {
            throw new System.NotImplementedException();
        }

        public void ShowContextMenu(CommandID menuID, int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public DesignerVerbCollection Verbs { get; }
    }
}