package mono.com.mikepenz.materialdrawer.adapter;


public class BaseDrawerAdapter_OnLongClickListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.mikepenz.materialdrawer.adapter.BaseDrawerAdapter.OnLongClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLongClick:(Landroid/view/View;ILcom/mikepenz/materialdrawer/model/interfaces/IDrawerItem;)Z:GetOnLongClick_Landroid_view_View_ILcom_mikepenz_materialdrawer_model_interfaces_IDrawerItem_Handler:Mikepenz.MaterialDrawer.Adapters.BaseDrawerAdapter/IOnLongClickListenerInvoker, material-drawer\n" +
			"";
		mono.android.Runtime.register ("Mikepenz.MaterialDrawer.Adapters.BaseDrawerAdapter+IOnLongClickListenerImplementor, material-drawer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BaseDrawerAdapter_OnLongClickListenerImplementor.class, __md_methods);
	}


	public BaseDrawerAdapter_OnLongClickListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BaseDrawerAdapter_OnLongClickListenerImplementor.class)
			mono.android.TypeManager.Activate ("Mikepenz.MaterialDrawer.Adapters.BaseDrawerAdapter+IOnLongClickListenerImplementor, material-drawer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public boolean onLongClick (android.view.View p0, int p1, com.mikepenz.materialdrawer.model.interfaces.IDrawerItem p2)
	{
		return n_onLongClick (p0, p1, p2);
	}

	private native boolean n_onLongClick (android.view.View p0, int p1, com.mikepenz.materialdrawer.model.interfaces.IDrawerItem p2);

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
