package md5968852844fdd935df9a71cf88c04df7d;


public class ResizePaddingTopAnimatorUpdateListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.animation.ValueAnimator.AnimatorUpdateListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAnimationUpdate:(Landroid/animation/ValueAnimator;)V:GetOnAnimationUpdate_Landroid_animation_ValueAnimator_Handler:Android.Animation.ValueAnimator/IAnimatorUpdateListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("BottomNavigationBar.Listeners.ResizePaddingTopAnimatorUpdateListener, BottomNavigationBar, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null", ResizePaddingTopAnimatorUpdateListener.class, __md_methods);
	}


	public ResizePaddingTopAnimatorUpdateListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ResizePaddingTopAnimatorUpdateListener.class)
			mono.android.TypeManager.Activate ("BottomNavigationBar.Listeners.ResizePaddingTopAnimatorUpdateListener, BottomNavigationBar, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public ResizePaddingTopAnimatorUpdateListener (android.view.View p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == ResizePaddingTopAnimatorUpdateListener.class)
			mono.android.TypeManager.Activate ("BottomNavigationBar.Listeners.ResizePaddingTopAnimatorUpdateListener, BottomNavigationBar, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onAnimationUpdate (android.animation.ValueAnimator p0)
	{
		n_onAnimationUpdate (p0);
	}

	private native void n_onAnimationUpdate (android.animation.ValueAnimator p0);

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
