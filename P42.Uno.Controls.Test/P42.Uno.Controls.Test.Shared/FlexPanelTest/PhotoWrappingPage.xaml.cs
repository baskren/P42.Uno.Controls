using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FlexPanelTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class PhotoWrappingPage : Page
    {
        class ImageList
        {
            [JsonProperty("photos")]
            public List<string> Photos { get; set; }
        }


        public PhotoWrappingPage()
        {
            this.InitializeComponent();
            LoadBitmapCollection();
        }


        async void LoadBitmapCollection()
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    // Download the list of stock photos
                    Uri listUri = new Uri("https://raw.githubusercontent.com/xamarin/docs-archive/master/Images/stock/small/stock.json");
                    byte[] listData = await webClient.DownloadDataTaskAsync(listUri);

                    // Convert to a Stream object
                    using (Stream listStream = new MemoryStream(listData))
                    {
                        // Deserialize the JSON into an ImageList object
                        //var jsonSerializer = new DataContractJsonSerializer(typeof(ImageList));
                        //ImageList imageList = (ImageList)jsonSerializer.ReadObject(listStream);
                        ImageList imageList = null;
                        using (var reader = new StreamReader(listStream)) 
                        using (var jsonReader = new JsonTextReader(reader))
                        {
                            var jsonSerializer = new JsonSerializer();
                            imageList = jsonSerializer.Deserialize<ImageList>(jsonReader);
                        }

                        // Create an Image object for each bitmap
                        foreach (string filepath in imageList.Photos)
                        {
                            var imageUri = new Uri(filepath, UriKind.Absolute);

                            byte[] imageBytes = await webClient.DownloadDataTaskAsync(imageUri);
                            var bitmapImage = await BitmapImageFromBytes(imageBytes);
                            var image = new Image { Stretch=Stretch.None, Source = bitmapImage };
                            flexPanel.Children.Add(image);
                        }
                    }
                }
                catch
                {
                    flexPanel.Children.Add(new TextBox
                    {
                        Text = "Cannot access list of bitmap files"
                    });
                }
            }

            activityIndicator.Visibility = Visibility.Collapsed;
        }

        async static Task<BitmapImage> BitmapImageFromBytes(Byte[] bytes)
        {
            var image = new BitmapImage();

#if WINDOWS_UWP || NETFX_CORE
            using (var stream = new InMemoryRandomAccessStream())
            {
                /*
                image.SetSource(stream);
                var s = stream.AsStreamForWrite();
                s.Write(bytes, 0, bytes.Length);
                */
                await stream.WriteAsync(bytes.AsBuffer());
                stream.Seek(0);
                await image.SetSourceAsync(stream);
            }
#else
            Stream stream = new MemoryStream(bytes);
            await image.SetSourceAsync(stream);
#endif
            return image;
        }



    }


}
