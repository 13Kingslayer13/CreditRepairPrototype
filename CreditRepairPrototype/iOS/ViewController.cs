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

			// Send request
			RequestHelper.Instance.SendRequest(
				WebAPI._uri_bitstamp, 
				WebAPI._api_tickerHour, 
				null, 
				Response, 
				BadResponse
			);

			// Send request and parse result in custom object
			RequestHelper.Instance.SendRequest<TickerResult>(
				WebAPI._uri_bitstamp,
				WebAPI._api_tickerHour,
				null,
				ResponseWithObject,
				BadResponse);
			
			// Send request for parse html page.
			// Parse only urls in 'class = "emphasized"'
			ParseHtmlSettings parseSettings = new ParseHtmlSettings("class", "emphasized");
			parseSettings.ContentInclusion = "http";
			RequestHelper.Instance.ReadHTMLPage (
				WebAPI._uri_bitstamp,
				"",
				null,
				parseSettings,
				ResponseWithHtmlPart,
				BadResponse
			);

			// Test database: update item in db
			User user = DatabaseAPI_iOS.Instance.GetUser ();
			if (user.Email == null) {
				Console.WriteLine ("Test database: User added!");
				user.Email = "ololo@test.net";
			}
			DatabaseAPI_iOS.Instance.UpdateUser (user);
			Console.WriteLine ("Test database: User updated - " + user.Email);
		}

		void Response(string data)
		{
			Console.WriteLine("Test simple request: " +  data);
		}

		void ResponseWithObject(TickerResult data)
		{
			Console.WriteLine("Test request with object-response: " +  data.Ask);
		}

		void ResponseWithHtmlPart(List<ParsedHtmlNode> data)
		{			
			Console.WriteLine("Test parsing HTML: successful! ");
			foreach (ParsedHtmlNode node in data) {
				//Console.WriteLine("URL from site: " +  node.NodeValue);
			}
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
