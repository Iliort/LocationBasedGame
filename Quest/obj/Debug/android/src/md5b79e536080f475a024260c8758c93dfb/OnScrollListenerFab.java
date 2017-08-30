package md5b79e536080f475a024260c8758c93dfb;


public class OnScrollListenerFab
	extends android.support.v7.widget.RecyclerView.OnScrollListener
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onScrolled:(Landroid/support/v7/widget/RecyclerView;II)V:GetOnScrolled_Landroid_support_v7_widget_RecyclerView_IIHandler\n" +
			"";
		mono.android.Runtime.register ("Quest.OnScrollListenerFab, Quest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", OnScrollListenerFab.class, __md_methods);
	}


	public OnScrollListenerFab () throws java.lang.Throwable
	{
		super ();
		if (getClass () == OnScrollListenerFab.class)
			mono.android.TypeManager.Activate ("Quest.OnScrollListenerFab, Quest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onScrolled (android.support.v7.widget.RecyclerView p0, int p1, int p2)
	{
		n_onScrolled (p0, p1, p2);
	}

	private native void n_onScrolled (android.support.v7.widget.RecyclerView p0, int p1, int p2);

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
