using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.SpeechSynthesis;
using DataAccessLibrary;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SmartToDoList
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {


        public MainPage()
        {
            this.InitializeComponent();
            WelcomeVoice(); //UNCOMMENT LATER
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MediaElement mediaElement = new MediaElement();
            var synth = new SpeechSynthesizer();
            mediaElement.Stop();
            this.Frame.Navigate(typeof(TaskPage),null);
        }
        private async void WelcomeVoice()
        {
            MediaElement mediaElement = new MediaElement();
            var synth = new SpeechSynthesizer();
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Welcome to the to do list manager. Click on Get Started to get going.");
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
            
        }
    }
}
