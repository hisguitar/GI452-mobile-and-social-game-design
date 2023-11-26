using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class InitWithDefault : MonoBehaviour
{
	async void Start()
	{
		await UnityServices.InitializeAsync();

		AskForConsent();
	}

	void AskForConsent()
	{
		// ... show the player a UI element that asks for consent.
		ConsentGiven(); // For test only
	}

	void ConsentGiven()
	{
		AnalyticsService.Instance.StartDataCollection();
	}
}