package md50866a52699e2aa36a6970890b6c8d6a9;


public class TextEditorActivity
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Quest.Activities.TextEditorActivity, Quest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TextEditorActivity.class, __md_methods);
	}


	public TextEditorActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == TextEditorActivity.class)
			mono.android.TypeManager.Activate ("Quest.Activities.TextEditorActivity, Quest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
