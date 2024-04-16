using ConsoleApp7.Buttons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.Components
{
    public class DataWindow : AppWindows
    {
        public int Selected = 0;
        public Table[] Table = { new Table(1), new Table(61) };
        public Button button;
        public DataWindow()
        {
            Table[0].application = this.Application;
            Table[1].application = this.Application;

            for (int i = 0; i < 2; i++)
            {
                Draw();
                SwitchWindow();
            }
        }

        public override void Refresh()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
			Table[0].application = this.Application;
			Table[1].application = this.Application;
			this.Table[Selected].Refresh(); // Zajištuje aby se zobrazili nově vytvořená data okamžitě
            this.Table[(Selected + 1) % 2].Refresh();

			for (int i = 0; i < 2; i++)
			{
				Draw();
				SwitchWindow();
			}
		}

        public override void HandleKey(ConsoleKeyInfo info)
        {

			string? input = info.Key.ToString();
            if (input.Contains('F') && input.Length == 2 || input == "F10")
                this.AddDialog(info);

            if (info.Key == ConsoleKey.Tab)
                SwitchWindow();

            else
                Table[Selected].HandleKey(info);


        }

        public override void Draw()
        {
            Table[Selected].Draw();
        }

        public void SwitchWindow()
        {
            Selected = (Selected + 1) % 2; // misto ifu a elsu tohle ezzzz
        }
        public void AddDialog(ConsoleKeyInfo info)
        {
            Button button = new Button(Table[Selected], Table[(Selected + 1) % 2], this.Application);
			this.Application.AddWindow(button);
			button.Application = this.Application;
			button.HandleKey(info);

		}
    }
}
