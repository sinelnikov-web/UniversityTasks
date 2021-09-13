using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        async private void button1_Click(object sender, EventArgs e)
        {
            var leftLens = listBox1.SelectedItem.ToString();
            var rightLens = listBox2.SelectedItem.ToString();
            var framesList = listView1.CheckedItems;
            string message = $"Левая линза: {leftLens}\nПравая линза: {rightLens}\nОправы:";
            int cost = 0;
            foreach (ListViewItem frame in framesList)
            {
                string itemText = frame.SubItems[0].Text;
                message += $"\n---------------------------\nНазвание: {Regex.Replace(itemText, @"[\s\n]+", " ")}\nЦена: {frame.SubItems[1].Text}";
                cost += int.Parse(frame.SubItems[1].Text.Trim().Replace(" ", ""));
            }
            message += $"\n---------------------------\nОбщая стоимость: {cost}";
            MessageBox.Show(message, caption: "Ваши покупки");
        }

        async Task<Dictionary<string, Dictionary<string, string>>> get_frames()
        {
            string url = "https://www.optic-city.ru/ochki_s_dioptrijami/katalog_oprav/";
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(url);
            var framesLinksList = doc.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[2]/div/div/a");
            var framesPhotosList = doc.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[2]/div/div/a/div[2]/img");
            var framesPriceList = doc.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[2]/div/div/ul/li[2]");
            Dictionary<string, Dictionary<string, string>> framesData = new Dictionary<string, Dictionary<string, string>>();
            for (int i = 0; i < framesPhotosList.Count; i++)
            {
                Dictionary<string, string> frameData = new Dictionary<string, string>();
                frameData.Add("photo", framesPhotosList[i].Attributes["src"].Value);
                frameData.Add("price", framesPriceList[i].InnerText.Trim());
                frameData.Add("url", framesLinksList[i].Attributes["href"].Value);
                framesData.Add($"{framesLinksList[i].InnerText.Replace("&nbsp;", "").Replace("&amp;", "").Trim()}{i+1}", frameData);
            }
            return framesData;
        }
        async private Task DownloadImagesFromWeb(string adress, ImageList il)
        {
            System.Net.WebRequest request = System.Net.WebRequest.Create(adress);
            System.Net.WebResponse resp = await request.GetResponseAsync();
            System.IO.Stream respStream = resp.GetResponseStream();
            Bitmap bmp = new Bitmap(respStream);
            respStream.Dispose();

            il.Images.Add(bmp);
        }

        async private void Form1_Load(object sender, EventArgs e)
        {

            for (int i = -10; i <= 10; i++)
            {
                listBox1.Items.Add(i.ToString());
                listBox2.Items.Add(i.ToString());
            }

            Dictionary<string, Dictionary<string, string>> framesList = await get_frames();
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(150, 150);
            int count = 0;
            foreach (var key in framesList.Keys)
            {
                await DownloadImagesFromWeb(framesList[key]["photo"], imageList);

            }
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.CheckBoxes = true;

            listView1.SmallImageList = imageList;
            foreach (var key in framesList.Keys)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(framesList[key]["price"]);
                item.Text = key;
                item.ImageIndex = count++;
                listView1.Items.Add(item);
            }
        }
    }
}
