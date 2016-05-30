using System;
using System.Collections.Generic;
using System.Reflection;
using UIKit;

namespace CreditRepairPrototype.iOS
{
	public partial class ViewController : UIViewController
	{
		int count = 1;

		public ViewController (IntPtr handle) : base (handle)
		{		
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Perform any additional setup after loading the view, typically from a nib.
			Button.AccessibilityIdentifier = "myButton";
			Button.TouchUpInside += delegate {
				var title = string.Format ("{0} clicks!", count++);
				Button.SetTitle (title, UIControlState.Normal);
			};
			// Test JSON reading
			List<Question> questions = JsonReader.Instance.ReadJsonArrayToList<Question>( Assembly.GetAssembly(typeof(JsonReader)), "data.json" );
			Console.WriteLine ("Test JSON: " + questions [0].QuestionString);
			// send request
			RequestHelper.Instance.SendRequest(
				WebAPI._uri_bitstamp, 
				WebAPI._api_tickerHour, 
				null, 
				Response, 
				BadResponse
			);
			// send request with specific object response
			RequestHelper.Instance.SendRequest<TickerResult>(
				WebAPI._uri_bitstamp,
				WebAPI._api_tickerHour,
				null,
				ResponseWithObject,
				BadResponse);
		}

		void Response(string data)
		{
			Console.WriteLine("Test simple request: " +  data);
		}

		void ResponseWithObject(TickerResult data)
		{
			Console.WriteLine("Test request with object-response: " +  data);
		}

		void BadResponse(string error)
		{
			Console.WriteLine(error);
		}

		public override void DidReceiveMemoryWarning ()
		{		
			base.DidReceiveMemoryWarning ();		
			// Release any cached data, images, etc that aren't in use.		
		}
	}   

	public class TickerResult
	{
		public decimal Last { get; set; }
		public decimal High { get; set; }
		public decimal Low { get; set; }
		public decimal Volume { get; set; }
		public decimal Bid { get; set; }
		public decimal Ask { get; set; }
	}
}
