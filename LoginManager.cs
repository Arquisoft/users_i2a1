using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour {



	[SerializeField]
	private InputField userField = null;
	[SerializeField]
	private InputField passField = null;
	[SerializeField]
	private Text feedbackMessage = null;
	[SerializeField]
	private Toggle rememberData = null;


	private string url = "http://localhost/login/login.php";


	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("lembra") && PlayerPrefs.GetInt ("lembra") == 1) {

			userField.text = PlayerPrefs.GetString("rememberLogin");
			passField.text = PlayerPrefs.GetString("rememberPass");
		}
	}
	
	// Update is called once per frame
	public void FazerLogin () {

		if (userField.text == "" || passField.text == "") {
				
			FeedBackError("EMPTY ");
		
				} else {


					string user = userField.text;
					string pass = passField.text;

					if (rememberData.isOn) {
						PlayerPrefs.SetInt ("lembra", 1);
						PlayerPrefs.SetString ("rememberLogin", user);

						PlayerPrefs.SetString ("rememberPass", pass);
					}



			WWW www = new WWW (url + "?login=" + user + "&password=" + pass); // strange
			//"http://localhost/login/login.php?login=xxx"

			StartCoroutine(ValidaLogin(www));
						
		}

	}

	IEnumerator ValidaLogin(WWW www)
	{
		yield return www;
		if (www.error == null) {
			if (www.text == "1")
			{
				FeedBackOk("OK");
				// Start Courutine
			}
			else
			{
				FeedBackError("WRONG");
			}


		} 
		else
		{
			if(www.error == "couldn`t connect to host")
			{
				FeedBackError("SERVER OFF");
			}


		}


	}










	IEnumerator LoadScene() {
		yield return new WaitForSeconds (5f);
		Application.LoadLevel ("Level");
	}

	void FeedBackOk(string mensagem){
		feedbackMessage.CrossFadeAlpha (100f, 0f, false);
		feedbackMessage.color = Color.green;
		feedbackMessage.text = mensagem;
	}

	void FeedBackError(string mensagem){
		feedbackMessage.CrossFadeAlpha (100f, 0f, false);
		feedbackMessage.color = Color.red;
		feedbackMessage.text = mensagem;
		feedbackMessage.CrossFadeAlpha (0f, 2f, false);
		userField.text = "";
		passField.text = "";
	}



}
