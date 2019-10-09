
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


	/// <summary>
	/// Player name input field. Let the user input his name, will appear above the player in the game.
	/// </summary>
	[RequireComponent(typeof(InputField))]
	public class PlayerNameInputField : MonoBehaviour
	{
		#region Private Constants

		const string playerNamePrefKey = "PlayerName";

		#endregion

		#region MonoBehaviour CallBacks
		
		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during initialization phase.
		/// </summary>
		void Start () {
		
			string defaultName = "Player";
			InputField _inputField = this.GetComponent<InputField>();

			if (_inputField!=null)
			{
				if (PlayerPrefs.HasKey(playerNamePrefKey))
				{
					
					defaultName = PlayerPrefs.GetString(playerNamePrefKey);
					
					
				}
				else{
					
					defaultName+=PhotonNetwork.CurrentRoom.PlayerCount+1;
				}
			}

			_inputField.text = defaultName;


			PhotonNetwork.NickName = defaultName;
		}

		#endregion
		
		#region Public Methods

		/// <summary>
		/// Sets the name of the player, and save it in the PlayerPrefs for future sessions.
		/// </summary>
		/// <param name="value">The name of the Player</param>
		public void SetPlayerName(InputField inputField)
		{
			string value = inputField.text;
			// #Important
		    if (string.IsNullOrEmpty(value))
		    {
                Debug.LogError("Player Name is null or empty");
		        return;
		    }
			PhotonNetwork.NickName = value;

			PlayerPrefs.SetString(playerNamePrefKey, value);
		}
		
		#endregion
	}

