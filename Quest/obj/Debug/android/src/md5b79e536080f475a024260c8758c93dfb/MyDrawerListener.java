package md5b79e536080f475a024260c8758c93dfb;


public class MyDrawerListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.mikepenz.materialdrawer.Drawer.OnDrawerItemClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onItemClick:(Landroid/view/View;ILcom/mikepenz/materialdrawer/model/interfaces/IDrawerItem;)Z:GetOnItemClick_Landroid_view_View_ILcom_mikepenz_materialdrawer_model_interfaces_IDrawerItem_Handler:Mikepenz.MaterialDrawer.Drawer/IOnDrawerItemClickListenerInvoker, material-drawer\n" +
			"";
		mono.android.Runtime.register ("Quest.MyDrawerListener, Quest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MyDrawerListener.class, __md_methods);
	}


	public MyDrawerListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MyDrawerListener.class)
			mono.android.TypeManager.Activate ("Quest.MyDrawerListener, Quest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public boolean onItemClick (android.view.View p0, int p1, com.mikepenz.materialdrawer.model.interfaces.IDrawerItem p2)
	{
		return n_onItemClick (p0, p1, p2);
	}

	private native boolean n_onItemClick (android.view.View p0, int p1, com.mikepenz.materialdrawer.model.interfaces.IDrawerItem p2);

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
