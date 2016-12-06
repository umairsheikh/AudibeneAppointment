using System;
using AVFoundation;
using Xamarin.Forms;
using TermineAudibene;

[assembly: Dependency (typeof (TextToSpeech_iOS))]

namespace TermineAudibene
{
	public class TextToSpeech_iOS : ITextToSpeech
	{
		public TextToSpeech_iOS ()
		{
		}

		float volume = 0.5f;
		float pitch = 1.0f;


		public void Speak (string text)
		{
			var speechSynthesizer = new AVSpeechSynthesizer ();

			var speechUtterance = new AVSpeechUtterance (text) {
				Rate = AVSpeechUtterance.MaximumSpeechRate / 4f,
				Voice = AVSpeechSynthesisVoice.FromLanguage ("en-US"),
				Volume = volume,
				PitchMultiplier = pitch
			};

			speechSynthesizer.SpeakUtterance (speechUtterance);
		}
	}
}

