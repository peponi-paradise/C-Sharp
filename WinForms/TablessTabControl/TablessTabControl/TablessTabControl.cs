using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace TablessTabControl
{
    public class CustomTabControl : TabControl
    {
        [Description("Show / hide tab"), Category("Behavior")]
        public bool ShowTab { get; set; } = true;

        private SynchronizationContext _synchronizationContext;

        public CustomTabControl()
        {
            // Get UI thread's synchronization context
            _synchronizationContext = SynchronizationContext.Current!;
        }

        protected override void WndProc(ref Message windowMessage)
        {
            if (ShowTab)
            {
                base.WndProc(ref windowMessage);
            }
            else
            {
                // Catch the `TCM_ADJUSTRECT` message
                // Check `DesignMode` for designer usage
                if (windowMessage.Msg == 0x1328 && !DesignMode)
                {
                    windowMessage.Result = 1;
                }
                else base.WndProc(ref windowMessage);
            }
        }

        public void AddTab(string name)
        {
            _synchronizationContext.Post(delegate
            {
                TabPages.Add(name, name);
                if (TabPages.Count == 1) SelectTab(name);
            }, null);
        }

        public void AddTab(TabPage page)
        {
            _synchronizationContext.Post(delegate
            {
                TabPages.Add(page);
                if (TabPages.Count == 1) SelectTab(page);
            }, null);
        }

        public void RemoveTab(string name)
        {
            _synchronizationContext.Post(delegate
            {
                TabPages.RemoveByKey(name);
            }, null);
        }

        public void RemoveTab(TabPage page)
        {
            _synchronizationContext.Post(delegate
            {
                TabPages.Remove(page);
            }, null);
        }

        public void ClearTabs()
        {
            _synchronizationContext.Post(delegate
            {
                TabPages.Clear();
            }, null);
        }

        public void NextTab()
        {
            _synchronizationContext.Post(delegate
            {
                if (SelectedIndex >= TabPages.Count - 1) return;
                SelectTab(SelectedIndex + 1);
            }, null);
        }

        public void PreviousTab()
        {
            _synchronizationContext.Post(delegate
            {
                if (SelectedIndex <= 0) return;
                SelectTab(SelectedIndex - 1);
            }, null);
        }

        public TabPage? this[int index]
        {
            get
            {
                if (TabPages.Count > 0) return TabPages[index];
                return null;
            }
        }

        public TabPage? this[string name]
        {
            get
            {
                if (TabPages.ContainsKey(name)) return TabPages[name];
                return null;
            }
        }
    }
}