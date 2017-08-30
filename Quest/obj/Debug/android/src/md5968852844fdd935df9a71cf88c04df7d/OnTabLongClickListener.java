package md5968852844fdd935df9a71cf88c04df7d;


public class OnTabLongClickListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.View.OnLongClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLongClick:(Landroid/view/View;)Z:GetOnLongClick_Landroid_view_View_Handler:Android.Views.View/IOnLongClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("BottomNavigationBar.Listeners.OnTabLongClickListener, BottomNavigationBar, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null", OnTabLongClickListener.class, __md_methods);
	}


	public OnTabLongClickListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == OnTabLongClickListener.class)
			mono.android.TypeManager.Activate ("BottomNavigationBar.Listeners.OnTabLongClickListener, BottomNavigationBar, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public boolean onLongClick (android.view.View p0)
	{
		return n_onLongClick (p0);
	}

	private native boolean n_onLongClick (android.view.View p0);

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
