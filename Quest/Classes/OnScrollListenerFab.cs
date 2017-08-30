using System.Collections.Generic;
using Android.Support.V7.Widget;
using Clans.Fab;

namespace Quest
{
    public class OnScrollListenerFab : RecyclerView.OnScrollListener
    {
        private List<FloatingActionButton> fabs = new List<FloatingActionButton>();
        private List<FloatingActionMenu> m_fabs = new List<FloatingActionMenu>();
        public OnScrollListenerFab() : base() { }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);
            if (dy > 0)
            {
                for (int i = 0; i < fabs.Count; i++) fabs[i].Hide(true);
                for (int i = 0; i < m_fabs.Count; i++)
                {
                    m_fabs[i].HideMenu(true);
                    m_fabs[i].HideMenuButton(true);
                }
            }

            else if (dy < 0)
            {
                for (int i = 0; i < fabs.Count; i++) fabs[i].Show(true);
                for (int i = 0; i < m_fabs.Count; i++)
                {
                    m_fabs[i].ShowMenu(true);
                    m_fabs[i].ShowMenuButton(true);
                }
            }

        }

        public void AddFab(FloatingActionButton fab)
        {
            fabs.Add(fab);
        }

        public void AddFabMenu(FloatingActionMenu fabMenu)
        {
            m_fabs.Add(fabMenu);
        }
    }

}