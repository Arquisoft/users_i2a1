using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mainController : MonoBehaviour {

	[SerializeField]
	private InputField userField = null;
	[SerializeField]
	private InputField passField = null;
	[SerializeField]
	private InputField emailField = null;
	[SerializeField]



	// GUI
	public Text statusText;
	public Text voteGuiStatusText;
	public GameObject loginCanvas;
	public CanvasGroup VoteOptionsCanvas;


	// Vote GUI

	public Slider opt1Slider;
	public Slider opt2Slider;
	public Slider opt3Slider;

	public GameObject voteCanvas;
	//public bool isVoted = false;
	public bool exists;


	// user info / php strings
	private string login = "http://localhost/votesys/login.php?";
	private string register = "http://localhost/votesys/register.php?"; //add a ? to your url
	private string vote = "http://localhost/votesys/vote.php?";
	private string voteCount = "http://localhost/votesys/votecount.php?";
	private string voteCount2 = "http://localhost/votesys/votecount2.php?";
	private string voteCount3 = "http://localhost/votesys/votecount3.php?";

	private string ableToVote = "http://localhost/votesys/abletovote.php?";
	private string countTotalUsers = "http://localhost/votesys/counttotalusers.php?";



	public string username;
	private string password;
	private string isVoted;

	public int totalUsersInt;
	public int voteOpt1resultInt;
	public int voteOpt2resultInt;
	public int voteOpt3resultInt;

	private string voteOption;

	public Text voteOption1results;
	public Text voteOption2results;
	public Text voteOption3results;

	public Text totalUsers;
	public Text totalUsers2;
	public Text totalUsers3;


	// Main function for execution coroutines
	public void executeCoroutines(){
		
		StartCoroutine(executionTimer());

	}

	public void updateVotes(){
		
		if(voteCanvas.activeInHierarchy == true)
		{
			Debug.Log("updating...");


			WWW voteCount1 = new WWW (voteCount); 

			WWW voteCount22 = new WWW (voteCount2); 

			WWW voteCount33 = new WWW (voteCount3); 

			WWW countTotal1 = new WWW (countTotalUsers); 

			StartCoroutine(countVotes(voteCount1));

			StartCoroutine(countVotes2(voteCount22));

			StartCoroutine(countVotes3(voteCount33));

			StartCoroutine(countTotalCoroutine(countTotal1));

		}

		
	}




	public void voteOption1 ()
	{

		username = userField.text;
		password = passField.text;
		voteOption = "1";
		isVoted = "1";

		WWW www = new WWW (vote + "&username=" + WWW.EscapeURL(username) + "&password=" + WWW.EscapeURL(password) 
			+ "&voteOption=" + WWW.EscapeURL(voteOption) + "&isVoted=" + WWW.EscapeURL(isVoted)
		); 

		StartCoroutine(voteOption1Coroutine(www));

		VoteOptionsCanvas.interactable = false;
		voteGuiStatusText.color = Color.white;
		voteGuiStatusText.text = "ALREADY VOTED";

	}

	public void voteOption2 ()
	{

		username = userField.text;
		password = passField.text;
		voteOption = "2";
		isVoted = "1";

		WWW www = new WWW (vote + "&username=" + WWW.EscapeURL(username) + "&password=" + WWW.EscapeURL(password) 
			+ "&voteOption=" + WWW.EscapeURL(voteOption) + "&isVoted=" + WWW.EscapeURL(isVoted)
		); 

		StartCoroutine(voteOption1Coroutine(www));

		VoteOptionsCanvas.interactable = false;
		voteGuiStatusText.color = Color.white;
		voteGuiStatusText.text = "ALREADY VOTED";

	}

	public void voteOption3 ()
	{

		username = userField.text;
		password = passField.text;
		voteOption = "3";
		isVoted = "1";

		WWW www = new WWW (vote + "&username=" + WWW.EscapeURL(username) + "&password=" + WWW.EscapeURL(password) 
			+ "&voteOption=" + WWW.EscapeURL(voteOption) + "&isVoted=" + WWW.EscapeURL(isVoted)
		); 

		StartCoroutine(voteOption1Coroutine(www));

		VoteOptionsCanvas.interactable = false;
		voteGuiStatusText.color = Color.white;
		voteGuiStatusText.text = "ALREADY VOTED";

	}



	public void registerNewUser()
	{

		checkUser();
	
	}



	public void loginUser()
	{
		username = userField.text;
		password = passField.text;


		WWW www = new WWW (login + "&username=" + WWW.EscapeURL(username) + "&password=" + WWW.EscapeURL(password)); 

		StartCoroutine(loginUser(www));

	}


	public void ableToVoteFunction(){

		username = userField.text;
		password = passField.text;

		WWW www = new WWW (ableToVote + "&username=" + WWW.EscapeURL(username) + "&password=" + WWW.EscapeURL(password)); 

		StartCoroutine(ableToVoteCoroutine(www));
	}


	public void checkUser(){

		username = userField.text;
		password = passField.text;


		WWW checker = new WWW (login + "&username=" + WWW.EscapeURL(username) + "&password=" + WWW.EscapeURL(password)); 

		StartCoroutine(checkUserCoroutine(checker));
		
	}


	public void goBack()
	{
		StartCoroutine(goBackCoroutine());
	}

	public void exit()
	{
		Application.Quit();
	}


	// Ienumerators
	IEnumerator ableToVoteCoroutine(WWW www)
	{

		yield return www;
		if (www.error == null) {
			if (www.text == "1")
			{
				
				VoteOptionsCanvas.interactable = true;
				voteGuiStatusText.color = Color.green;
				voteGuiStatusText.text = "ALLOWED TO VOTE";

			}
			else
			{

				VoteOptionsCanvas.interactable = false;
				voteGuiStatusText.color = Color.white;
				voteGuiStatusText.text = "ALREADY VOTED";
			}


		} 
		else
		{
			if(www.error == "couldn`t connect to host")
			{
				Debug.Log("SERVER OFF");
			}


		}

		
	}


	IEnumerator countVotes(WWW www){

		yield return www;
		Debug.Log(www.text);
		voteOption1results.text = www.text;

		if (www.text == "")
		{
			opt1Slider.value = 0;
		}

		int votes1 = int.Parse(www.text);

		opt1Slider.value = votes1;


	}

	IEnumerator countVotes2(WWW www){

		yield return www;
		Debug.Log(www.text);
		voteOption2results.text = www.text;

		if (www.text == "")
		{
			opt2Slider.value = 0;
		}


		int votes2 = int.Parse(www.text);

		opt2Slider.value = votes2;


	}

	IEnumerator countVotes3(WWW www){

		yield return www;
		Debug.Log(www.text);
		voteOption3results.text = www.text;

		if (www.text == "")
		{
			opt3Slider.value = 0;
		}


		int votes3 = int.Parse(www.text);

		opt3Slider.value = votes3;


	}
		

	IEnumerator countTotalCoroutine(WWW www){

		yield return www;
		Debug.Log(www.text);
		if (www.text == "0")
		{
			opt1Slider.maxValue = 0;
			opt2Slider.maxValue = 0;
			opt3Slider.maxValue = 0;
		}
		totalUsers.text = " / " + www.text;
		totalUsers2.text = " / " + www.text;
		totalUsers3.text = " / " + www.text;


		int totalUsersInt = int.Parse(www.text);


		opt1Slider.maxValue = totalUsersInt;
		opt2Slider.maxValue = totalUsersInt;
		opt3Slider.maxValue = totalUsersInt;


	}


	IEnumerator executionTimer()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.5f);
			updateVotes();
		}
		
	}


	IEnumerator GoVote()
	{
		yield return new WaitForSeconds(0.5f);
		loginCanvas.SetActive(false);
		voteCanvas.SetActive(true);

		// check user
		VoteOptionsCanvas.interactable = false;
		ableToVoteFunction();


	}
		

	IEnumerator goBackCoroutine()
	{
		yield return new WaitForSeconds(0.5f);
		statusText.color = Color.white;
		statusText.text = "STATUS";
		loginCanvas.SetActive(true);
		voteCanvas.SetActive(false);


	}


	IEnumerator loginUser(WWW www)
	{
		yield return www;
		if (www.error == null) {
			if (www.text == "1")
			{
				Debug.Log("OK");
				statusText.text = "OK";
				statusText.color = Color.green;

				StartCoroutine(GoVote());


			}
			else
			{
				
				statusText.text = "WRONG DATA";
				statusText.color = Color.red;
				Debug.Log(www.text);
			}


		} 
		else
		{
			if(www.error == "couldn`t connect to host")
			{
				Debug.Log("SERVER OFF");
			}


		}
	}


	IEnumerator checkUserCoroutine(WWW checker)
	{
		
		yield return checker;


		if (checker.error == null) {

			if (checker.text == "1")
				
			{
				exists = true;

				Debug.Log("ALREADY EXISTS");

				statusText.text = "ALREADY EXISTS";
				statusText.color = Color.red;


			}
			else
			{

				Debug.Log("registering");

				exists = false;

				StartCoroutine(registerNewCoroutine(username,password));

			}


		} 
		else
		{
			

			if(checker.error == "couldn`t connect to host")
			{
				Debug.Log("SERVER OFF");
			}


		}
	}


	IEnumerator registerNewCoroutine(string username, string password)
	{


		

		//This connects to a server side php script that will write the data
		string post_url = register + "&username=" + WWW.EscapeURL(username) + "&password=" + WWW.EscapeURL(password);
		//WWW.EscapeURL(username)


		//string post_url = postDataURL + "name=" + WWW.EscapeURL(name) + "&data=" + psw ;

		// Post the URL to the site and create an upload object to get the result.
		WWW data_post = new WWW(post_url);
		yield return data_post; // Wait until the upload is done
		Debug.Log("data posted successfully");
		statusText.text = "REGISTERED";
		statusText.color = Color.green;
		statusText.CrossFadeAlpha (100f, 2f, false);

		if (data_post.error != null)
		{
			Debug.Log("There was an error saving data: " + data_post.error);
		}
		Debug.Log("REGISTERED");
		Debug.Log(password);

	
	}


	IEnumerator voteOption1Coroutine(WWW www)
	{

		yield return (www); // Wait until the upload is done
		Debug.Log("votedOption1");

	}






	// Use this for initialization
	void Start () {

		exists = false;
		
		voteCanvas.SetActive(false);

		executeCoroutines();
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}




}
