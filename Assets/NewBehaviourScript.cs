using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Soomla.Profile;

public class NewBehaviourScript : MonoBehaviour {

	public Text CurrentProviderText;

	Provider TargetProvider = Provider.TWITTER; // SOOMLA Test 816918098367233

	// Use this for initialization
	void Start () {
		ProfileEvents.OnSoomlaProfileInitialized += delegate() {
			Debug.Log("OnSoomlaProfileInitialized");
		};
		ProfileEvents.OnSocialActionStarted += delegate(Provider provider, SocialActionType action, string payload) {
			Debug.Log("OnSocialActionStarted");
		};
		ProfileEvents.OnSocialActionFinished += delegate(Provider provider, SocialActionType action, string payload) {
			Debug.Log("OnSocialActionFinished");
		};
		ProfileEvents.OnSocialActionCancelled += delegate(Provider provider, SocialActionType action, string payload) {
			Debug.Log("OnSocialActionCancelled");
		};
		ProfileEvents.OnSocialActionFailed += delegate(Provider provider, SocialActionType action, string message, string payload) {
			Debug.Log("OnSocialActionFailed");
		};
		SoomlaProfile.Initialize();
		SyncProvider();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SyncProvider(Provider provider = null) {
		if (provider != null) {
			TargetProvider = provider;
		}
		CurrentProviderText.text = TargetProvider.ToString();
	}

	public void OnLoginButton() {
		Action unsubscribeAll = delegate() {
        };
        
		Action<Provider, bool, string> onLoginStarted = delegate(Provider provider, bool autoLogin, string payload) {
			Debug.Log("OnLoginStarted");
        };
        Action<Provider, bool, string> onLoginCancelled = delegate(Provider provider, bool autoLogin, string payload) {
			Debug.Log("onLoginCancelled");
			unsubscribeAll();
		};
		Action<Provider, string, bool, string> onLoginFailed = delegate(Provider provider, string message, bool autoLogin, string payload) {
			Debug.Log("onLoginFailed");
			unsubscribeAll();
        };
		Action<UserProfile, bool, string> onLoginFinished = delegate(UserProfile userProfile, bool autoLogin, string payload) {
			Debug.Log("OnLoginFinished");
			unsubscribeAll();
        };

		unsubscribeAll = delegate() {
            ProfileEvents.OnLoginStarted -= onLoginStarted;
			ProfileEvents.OnLoginCancelled -= onLoginCancelled;
			ProfileEvents.OnLoginFailed -= onLoginFailed;
			ProfileEvents.OnLoginFinished -= onLoginFinished;
        };
        
        ProfileEvents.OnLoginStarted += onLoginStarted;
		ProfileEvents.OnLoginCancelled += onLoginCancelled;
		ProfileEvents.OnLoginFailed += onLoginFailed;
		ProfileEvents.OnLoginFinished += onLoginFinished;

		SoomlaProfile.Login(TargetProvider);
    }

	public void OnIsLoginButton() {
		Debug.Log("IsLogin returns: " + SoomlaProfile.IsLoggedIn(TargetProvider));
	}

	public void OnLogoutButton() {
		Action unsubscribeAll = delegate() {
		};
		
		Action<Provider> onLogoutStarted = delegate(Provider provider) {
			Debug.Log("onLogoutStarted");
		};
		Action<Provider, string> onLogoutFailed = delegate(Provider provider, string message) {
			Debug.Log("onLogoutFailed");
			unsubscribeAll();
		};
		Action<Provider> onLogoutFinished = delegate(Provider provider) {
			Debug.Log("onLogoutFinished");
			unsubscribeAll();
		};
		
		ProfileEvents.OnLogoutStarted += onLogoutStarted;
		ProfileEvents.OnLogoutFailed += onLogoutFailed;
		ProfileEvents.OnLogoutFinished += onLogoutFinished;
		
		unsubscribeAll = delegate() {
			ProfileEvents.OnLogoutStarted -= onLogoutStarted;
			ProfileEvents.OnLogoutFailed -= onLogoutFailed;
			ProfileEvents.OnLogoutFinished -= onLogoutFinished;
        };
        
		SoomlaProfile.Logout(TargetProvider);
    }
    
	public void OnGetContactsButton() {
        Action unsubscribeAll = delegate() {
		};
        
        Action<Provider, bool, string> onGetContactsStarted = delegate(Provider provider, bool fromStart, string payload) {
			Debug.Log("onGetContactsStarted");
		};
		Action<Provider, string, bool, string> onGetContactsFailed = delegate(Provider provider, string message, bool fromStart, string payload) {
            Debug.Log("onGetContactsFailed");
			unsubscribeAll();
        };
		Action<Provider, SocialPageData<UserProfile>, string> onGetContactsFinished = delegate(Provider provider, SocialPageData<UserProfile> userProfiles, string payload) {
			Debug.Log("onGetContactsFinished");
			unsubscribeAll();
        };
        
		ProfileEvents.OnGetContactsStarted += onGetContactsStarted;
		ProfileEvents.OnGetContactsFailed += onGetContactsFailed;
		ProfileEvents.OnGetContactsFinished += onGetContactsFinished;

		unsubscribeAll = delegate() {
            ProfileEvents.OnGetContactsStarted -= onGetContactsStarted;
			ProfileEvents.OnGetContactsFailed -= onGetContactsFailed;
			ProfileEvents.OnGetContactsFinished -= onGetContactsFinished;
		};
        
        SoomlaProfile.GetContacts(TargetProvider, true);
	}

	public void OnLike() {
		SoomlaProfile.Like(TargetProvider, "100001710521007");
    }

	public void OnUpdateStatus() {
		SoomlaProfile.UpdateStatus(TargetProvider, "I LOVE SOOMLA!  http://www.soom.la");
    }

	public void OnUpdateStory() {
		SoomlaProfile.UpdateStory(TargetProvider, 
		                          "This is the story.",                       // Text of the story to post
		                          "The story of SOOMBOT (Profile Test App)",  // Name
		                          "SOOMBOT Story",                            // Caption
		                          "Hey! It's SOOMBOT Story",                 	// Description
                                  "http://about.soom.la/soombots",            // Link to post
                                  "http://about.soom.la/.../spockbot.png"     // Image URL
		                          );
    }

	public void OnGetUserProfile() {
		SoomlaProfile.GetStoredUserProfile(TargetProvider);
	}
    
	public void OnOpenAppRatingPage() {
		SoomlaProfile.OpenAppRatingPage();
	}

	public void OnMultiShare() {
		SoomlaProfile.MultiShare(
			"I'm happy. I can be shared everywhere.");
	}
	
	public void OnSwitchToFb() {
        SyncProvider(Provider.FACEBOOK);
	}

	public void OnSwitchToTwitter() {
		SyncProvider(Provider.TWITTER);
    }

	public void OnSwitchToGoogle() {
		SyncProvider(Provider.GOOGLE);
    }

}
