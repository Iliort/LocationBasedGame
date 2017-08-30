package md50866a52699e2aa36a6970890b6c8d6a9;


public class TaskItemViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Quest.Activities.TaskItemViewHolder, Quest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TaskItemViewHolder.class, __md_methods);
	}


	public TaskItemViewHolder (android.view.View p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == TaskItemViewHolder.class)
			mono.android.TypeManager.Activate ("Quest.Activities.TaskItemViewHolder, Quest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
