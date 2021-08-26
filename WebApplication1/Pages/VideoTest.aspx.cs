using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Pages
{
        public partial class VideoTest : System.Web.UI.Page
        {
                protected void Page_Load(object sender, EventArgs e)
                {

                }

                protected void btDownload_Click(object sender, EventArgs e)
                {
                        try
                        {
                                string url = "";

                                //Try to get url from textbox
                                if (txtUrl.Text == "")
                                {
                                        lbError.Text = "No video found";
                                        return;
                                }
                                else
                                {
                                        url = txtUrl.Text.ToString();
                                        lbError.Text = "";
                                }

                                if (url == "")
                                {
                                        lbError.Text = "No video found";
                                        return;
                                }

                                //Begin downloading video

                                DownloadVideoV3(url, "Test");
                        }
                        catch (Exception ex)
                        {
                                lbError.Text = "An error has unexpectedly occured; " + ex.Message;
                        }
                }

                protected void txtUrl_TextChanged(object sender, EventArgs e)
                {
                        lbError.Text = "";
                }

                public void DownloadVideoV3(string url, string fileName)
                {
                        //https://www.ecanarys.com/Blogs/ArticleID/77/How-to-download-YouTube-Videos-using-

                        Uri videoUri = new Uri(url);
                        string videoID = HttpUtility.ParseQueryString(videoUri.Query).Get("v");
                        string videoInfoUrl = "http://www.youtube.com/get_video_info?video_id=" + videoID;



                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(videoInfoUrl);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();



                        Stream responseStream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));



                        string videoInfo = HttpUtility.HtmlDecode(reader.ReadToEnd());



                        NameValueCollection videoParams = HttpUtility.ParseQueryString(videoInfo);



                        if (videoParams["reason"] != null)
                        {
                                lbError.Text = videoParams["reason"];
                                return;
                        }



                        string[] videoURLs = videoParams["url_encoded_fmt_stream_map"].Split(',');



                        foreach (string vURL in videoURLs)
                        {
                                string sURL = HttpUtility.HtmlDecode(vURL);



                                NameValueCollection urlParams = HttpUtility.ParseQueryString(sURL);
                                string videoFormat = HttpUtility.HtmlDecode(urlParams["type"]);



                                sURL = HttpUtility.HtmlDecode(urlParams["url"]);
                                sURL += "&signature=" + HttpUtility.HtmlDecode(urlParams["sig"]);
                                sURL += "&type=" + videoFormat;
                                sURL += "&title=" + HttpUtility.HtmlDecode(videoParams["title"]);



                                videoFormat = urlParams["quality"] + " - " + videoFormat.Split(';')[0].Split('/')[1];



                                //ddlVideoFormats.Items.Add(new ListItem(videoFormat, sURL));
                        }



                        //btnProcess.Enabled = false;
                        //ddlVideoFormats.Visible = true;
                        //btnDownload.Visible = true;
                        //lblMessage.Text = string.Empty;
                }
                public void DownloadVideoV2(string url, string fileName)
                {
                        WebClient req = new WebClient();
                        HttpResponse response = HttpContext.Current.Response;
                        response.Clear();
                        response.ClearContent();
                        response.ClearHeaders();
                        response.Buffer = true;
                        response.AddHeader("Content-Disposition", "attachment;filename=\"" + Server.MapPath(url) + "\"");
                        byte[] data = req.DownloadData(Server.MapPath(url));
                        response.BinaryWrite(data);
                        response.End();

                }
                public int DownloadVideoV1(string url, string fileName)
                {
                        int result = 0;
                        //Create a stream for the file
                        Stream stream = null;

                        //This controls how many bytes to read at a time and send to the client
                        //int bytesToRead = 10000;
                        int bytesToRead = 90000;

                        // Buffer to read bytes in chunk size specified above
                        byte[] buffer = new Byte[bytesToRead];

                        // The number of bytes read
                        try
                        {
                                //Create a WebRequest to get the file
                                HttpWebRequest fileReq = (HttpWebRequest)HttpWebRequest.Create(url);

                                //Create a response for this request
                                HttpWebResponse fileResp = (HttpWebResponse)fileReq.GetResponse();

                                if (fileReq.ContentLength > 0)
                                        fileResp.ContentLength = fileReq.ContentLength;

                                //Get the Stream returned from the response
                                stream = fileResp.GetResponseStream();

                                // prepare the response to the client. resp is the client Response
                                var resp = HttpContext.Current.Response;

                                //Indicate the type of data being sent
                                resp.ContentType = "application/octet-stream";

                                //Name the file
                                resp.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + ".mp4\"");
                                resp.AddHeader("Content-Length", fileResp.ContentLength.ToString());

                                int length;
                                do
                                {
                                        // Verify that the client is connected.
                                        if (resp.IsClientConnected)
                                        {
                                                // Read data into the buffer.
                                                length = stream.Read(buffer, 0, bytesToRead);

                                                // and write it out to the response's output stream
                                                resp.OutputStream.Write(buffer, 0, length);

                                                // Flush the data
                                                resp.Flush();

                                                //Clear the buffer
                                                buffer = new Byte[bytesToRead];
                                        }
                                        else
                                        {
                                                // cancel the download if client has disconnected
                                                length = -1;
                                        }
                                } while (length > 0); //Repeat until no data is read
                        }
                        finally
                        {
                                if (stream != null)
                                {
                                        //Close the input stream
                                        stream.Close();
                                        result = 1;
                                }
                        }
                        return result;
                }
        }
}