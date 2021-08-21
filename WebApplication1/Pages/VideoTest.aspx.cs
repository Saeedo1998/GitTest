﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

                                DownloadVideo(url, "Test");
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

                public int DownloadVideo(string url, string fileName)
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