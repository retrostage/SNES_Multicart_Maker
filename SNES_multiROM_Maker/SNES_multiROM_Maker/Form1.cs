using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MultiCartMaker
{
    public partial class SNESmulticartMaker : Form
    {
        //buffers for each ROM
        byte[] snesHeader;
        byte[] snes1;
        byte[] snes2;
        byte[] snes3;
        byte[] snes4;
        byte[] multigame;
        string selectedDirectory;

        int is8mbflash = 8388608;
        int is16mbflash = 16777216;
        int maxSize;
        int sizeLeft;
        int firstSize = 0;
        int secondSize = 0;
        int thirdSize = 0;
        int fourthSize = 0;
        int currSize = 0;

        //flags for Blaster size
        bool is8mb = false;
        bool is16mb = false;

        bool game1Selected = false;
        bool game2Selected = false;
        bool game3Selected = false;
        bool game4Selected = false;

        //flags for how many games are selected
        int gamesSelected = 0;

        public delegate void ReportProgressDelegate(int percentage);

        public void report(ReportProgressDelegate reportProgress)
        {
            for (int i = 0; i < 100; i++)
            {
                reportProgress(i);
            }
        }

        public SNESmulticartMaker()
        {
            InitializeComponent();
            pictureBox1.Image = MultiCartMaker.Properties.Resources.smalllogo2;

            this.Load += Form1_Load;

        }

        void Form1_Load(object sender, EventArgs e)
        {
            if (eightmb.Checked)
            {
                progressBar1.BeginInvoke(new Action(() => { progressBar1.Maximum = 8 * 1024 * 1024; }));
                is8mb = true;
                is16mb = false;
            }
            else
            {
                progressBar1.BeginInvoke(new Action(() => { progressBar1.Maximum = 16 * 1024 * 1024; }));
                is8mb = false;
                is16mb = true;
            }
        }

        void capacityChanged(int size)
        {
            if (size == 8)
            {
                progressBar1.BeginInvoke(new Action(() => { progressBar1.Maximum = is8mbflash; }));
                is8mb = true;
                is16mb = false;
            }
            else
            {
                progressBar1.BeginInvoke(new Action(() => { progressBar1.Maximum = is16mbflash; }));
                is8mb = false;
                is16mb = true;
            }
        }

        private void BuildROM_Click(object sender, EventArgs e)
        {
            byte[] header = new byte[512];
            byte command = 0xaa;
            int mapType1 = 0;
            int mapType2 = 0;
            int mapType3 = 0;
            int mapType4 = 0;
            int sram1 = 0;
            int sram2 = 0;
            int sram3 = 0;
            int sram4 = 0;
            int is4mb = 0;
            int mapper;
            byte gamesAvailable;
            int gamesNumber = 0;            

            if (game1Selected)
            {
                mapType1 = checkHeaderType(snes1);
                sram1 = checkHeaderSRAM(snes1);
                gamesNumber = 1;
            }

            if (game2Selected)
            {
                mapType2 = checkHeaderType(snes2);
                sram2 = checkHeaderSRAM(snes2);
                gamesNumber = 2;
            }

            if (game3Selected)
            {
                mapType3 = checkHeaderType(snes3);
                sram3 = checkHeaderSRAM(snes3);
                gamesNumber = 3;
            }

            if (game4Selected)
            {
                mapType4 = checkHeaderType(snes4);
                sram4 = checkHeaderSRAM(snes4);
                gamesNumber = 4;
            }

            if (gamesNumber < 2)
            {
                MessageBox.Show("You need to select more than one game for multi-cart mode.");
            }
            else if (gamesNumber == 2)
            {
                //if each of the two games are smaller than 2MB, keep ROM size 4MB (programs faster, and we don't need 4MB of padding)
                if (snes1.Length <= 2097152 && snes2.Length <= 2097152)
                {
                    gamesAvailable = 0x22;
                    is4mb = 0;
                    //fix for v0.02
                    //need space for the header...duh.
                    multigame = new byte[4194816];
                }
                else
                {
                    gamesAvailable = 0x42;
                    is4mb = 1;
                    //fix for v0.02
                    //need space for the header...duh.
                    multigame = new byte[8389120];
                }

                byte hasSRAM = 0;

                if(sram1 == 1 || sram2 == 1)
                {
                    hasSRAM = 1;
                }
                mapper = is4mb << 6 | 0 << 5 | 0 << 4 | mapType2 << 3 | mapType1 << 2 | 1 << 1 | hasSRAM;

                header[0] = 0xaa;
                header[1] = Convert.ToByte(mapper);
                header[2] = gamesAvailable;

                for (int c = 3; c < 512; c++)
                {
                    header[c] = 0xff;
                }

                //if(snes1.Length < 4194304)
                //{
                //    Array.ConstrainedCopy(header, 0, multigame, 0, 512);
                //    Array.ConstrainedCopy(snes1, 0, multigame, 512, snes1.Length);
                //    Array.ConstrainedCopy(snes1, 0, multigame, 512 + snes1.Length, snes1.Length);
                //    Array.ConstrainedCopy(snes2, 0, multigame, snes1.Length + snes1.Length + 512, snes2.Length);
                //}
                //else if (snes2.Length < 4194304)
                //{
                //    Array.ConstrainedCopy(header, 0, multigame, 0, 512);
                //    Array.ConstrainedCopy(snes1, 0, multigame, 512, snes1.Length);                    
                //    Array.ConstrainedCopy(snes2, 0, multigame, snes1.Length + 512, snes2.Length);
                //    Array.ConstrainedCopy(snes2, 0, multigame, snes1.Length + snes2.Length + 512, snes2.Length);
                //}

                //fix for v0.03
                if (is4mb == 0)
                {
                    Array.ConstrainedCopy(header, 0, multigame, 0, 512);
                    Array.ConstrainedCopy(snes1, 0, multigame, 512, snes1.Length);
                    Array.ConstrainedCopy(snes2, 0, multigame, snes1.Length + 512, snes2.Length);
                }
                else
                {
                    Array.ConstrainedCopy(header, 0, multigame, 0, 512);
                    Array.ConstrainedCopy(snes1, 0, multigame, 512, snes1.Length);
                    Array.ConstrainedCopy(snes2, 0, multigame, snes1.Length + 512, snes2.Length);
                }                

                SaveFileDialog saveFileNow = new SaveFileDialog();
                saveFileNow.InitialDirectory = "C:/";
                saveFileNow.Filter = "All files (*.*)|*.*|SNES Save files |*.sfc";
                saveFileNow.FilterIndex = 2;
                saveFileNow.DefaultExt = ".sfc";
                saveFileNow.RestoreDirectory = true;

                if (saveFileNow.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(saveFileNow.FileName))
                    {
                        File.Delete(saveFileNow.FileName);
                    }
                    FileStream stream = new FileStream(saveFileNow.FileName, FileMode.Append, FileAccess.Write);
                    stream.Write(multigame, 0, multigame.Length);
                    stream.Flush();
                    stream.Close();
                    MessageBox.Show("Multi-Cart ROM created!");
                    progressBar1.BeginInvoke(new Action(() => { progressBar1.Value = 0; }));
                    firsttext.Text = " ";
                    secondtext.Text = " ";
                    game1Selected = false;
                    game2Selected = false;
                    game3Selected = false;
                    game4Selected = false;
                    gamesSelected = 0;
                    currSize = 0;
                }
                else
                {
                    MessageBox.Show("No file created.");
                }
            }
            else if (gamesNumber == 3)
            {
                //multigame = new byte[snes1.Length + snes2.Length + snes3.Length + snes3.Length + 512];

                if (is8mb)
                {
                    gamesAvailable = 0x23;
                    is4mb = 0;
                    multigame = new byte[snes1.Length + snes2.Length + snes3.Length + snes3.Length + 512];
                }
                else
                {
                    gamesAvailable = 0x43;
                    is4mb = 1;
                    multigame = new byte[snes1.Length + snes2.Length + snes3.Length + 512];
                }

                byte hasSRAM = 0;

                if (sram1 == 1 || sram2 == 1 || sram3 == 1)
                {
                    hasSRAM = 1;
                }

                mapper = is4mb << 6 | 0 << 5 | mapType3 << 4 | mapType2 << 3 | mapType1 << 2 | 1 << 1 | hasSRAM;

                header[0] = 0xaa;
                header[1] = Convert.ToByte(mapper);
                header[2] = gamesAvailable;

                for (int c = 3; c < 512; c++)
                {
                    header[c] = 0xff;
                }
                if(firstSize + secondSize + thirdSize < 8388608)
                {
                    Array.ConstrainedCopy(header, 0, multigame, 0, 512);
                    Array.ConstrainedCopy(snes1, 0, multigame, 512, snes1.Length);
                    Array.ConstrainedCopy(snes2, 0, multigame, snes1.Length + 512, snes2.Length);
                    Array.ConstrainedCopy(snes3, 0, multigame, snes1.Length + snes2.Length + 512, snes3.Length);
                    Array.ConstrainedCopy(snes3, 0, multigame, snes1.Length + snes2.Length + snes3.Length + 512, snes3.Length);
                }
                else
                {
                    Array.ConstrainedCopy(header, 0, multigame, 0, 512);
                    Array.ConstrainedCopy(snes1, 0, multigame, 512, snes1.Length);
                    Array.ConstrainedCopy(snes2, 0, multigame, snes1.Length + 512, snes2.Length);
                    Array.ConstrainedCopy(snes3, 0, multigame, snes1.Length + snes2.Length + 512, snes3.Length);
                }            

                SaveFileDialog saveFileNow = new SaveFileDialog();
                saveFileNow.InitialDirectory = "C:/";
                saveFileNow.Filter = "All files (*.*)|*.*|SNES Save files |*.sfc";
                saveFileNow.FilterIndex = 2;
                saveFileNow.DefaultExt = ".sfc";
                saveFileNow.RestoreDirectory = true;

                if (saveFileNow.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(saveFileNow.FileName))
                    {
                        File.Delete(saveFileNow.FileName);
                    }
                    FileStream stream = new FileStream(saveFileNow.FileName, FileMode.Append, FileAccess.Write);
                    stream.Write(multigame, 0, multigame.Length);
                    stream.Flush();
                    stream.Close();
                    MessageBox.Show("Multi-Cart ROM created!");
                    progressBar1.BeginInvoke(new Action(() => { progressBar1.Value = 0; }));
                    firsttext.Text = " ";
                    secondtext.Text = " ";
                    thirdtext.Text = " ";
                    game1Selected = false;
                    game2Selected = false;
                    game3Selected = false;
                    game4Selected = false;
                    gamesSelected = 0;
                    currSize = 0;
                }
                else
                {
                    MessageBox.Show("No file created.");
                }
            }
            else if (gamesNumber == 4)
            {
                multigame = new byte[snes1.Length + snes2.Length + snes3.Length + snes4.Length + 512];

                if (is8mb)
                {
                    gamesAvailable = 0x24;
                    is4mb = 0;
                }
                else
                {
                    gamesAvailable = 0x44;
                    is4mb = 1;
                }

                byte hasSRAM = 0;

                if (sram1 == 1 || sram2 == 1 || sram3 == 1 || sram4 == 1)
                {
                    hasSRAM = 1;
                }

                mapper = is4mb << 6 | mapType4 << 5 | mapType3 << 4 | mapType2 << 3 | mapType1 << 2 | 1 << 1 | hasSRAM;

                header[0] = 0xaa;
                header[1] = Convert.ToByte(mapper);
                header[2] = gamesAvailable;

                for (int c = 3; c < 512; c++)
                {
                    header[c] = 0xff;
                }
                Array.ConstrainedCopy(header, 0, multigame, 0, 512);
                Array.ConstrainedCopy(snes1, 0, multigame, 512, snes1.Length);
                Array.ConstrainedCopy(snes2, 0, multigame, snes1.Length + 512, snes2.Length);
                Array.ConstrainedCopy(snes3, 0, multigame, snes1.Length + snes2.Length + 512, snes3.Length);
                Array.ConstrainedCopy(snes4, 0, multigame, snes1.Length + snes2.Length + snes3.Length + 512, snes4.Length);

                SaveFileDialog saveFileNow = new SaveFileDialog();
                saveFileNow.InitialDirectory = "C:/";
                saveFileNow.Filter = "All files (*.*)|*.*|SNES Save files |*.sfc";
                saveFileNow.FilterIndex = 2;
                saveFileNow.DefaultExt = ".sfc";
                saveFileNow.RestoreDirectory = true;

                if (saveFileNow.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(saveFileNow.FileName))
                    {
                        File.Delete(saveFileNow.FileName);
                    }
                    FileStream stream = new FileStream(saveFileNow.FileName, FileMode.Append, FileAccess.Write);
                    stream.Write(multigame, 0, multigame.Length);
                    stream.Flush();
                    stream.Close();
                    MessageBox.Show("Multi-Cart ROM created!");
                    progressBar1.BeginInvoke(new Action(() => { progressBar1.Value = 0; }));
                    firsttext.Text = " ";
                    secondtext.Text = " ";
                    thirdtext.Text = " ";
                    fourthtext.Text = " ";
                    game1Selected = false;
                    game2Selected = false;
                    game3Selected = false;
                    game4Selected = false;
                    gamesSelected = 0;
                    currSize = 0;
                }
                else
                {
                    MessageBox.Show("No file created.");
                }
            }
        }

        private int checkHeaderType(byte[] game)
        {
            byte isHirom;
            byte isHirom2;
            byte isLorom;
            int configByte = 0;

            if (game.Length != 0)
            {
                isHirom = game[0xffd5];
                isHirom2 = game[0xffd4];
                isLorom = game[0x7fd5];


                if ((isHirom == 0x21 || isHirom == 0x25 || isHirom == 0x31 || isHirom == 0x35) && isHirom2 == 0x20 || isHirom2 == 0x70 || isHirom2 == 0x33) /*fixes a few games*/
                {
                    configByte = 1; //HIROM
                }
                else if ((isLorom == 0x20 || isLorom == 0x30))
                {
                    configByte = 0; //LOROM
                }
                else
                {
                    MessageBox.Show("Mapper type not found. Possibly bad ROM header?");
                }
            }
            else
            {
                MessageBox.Show("Game not found!");
            }
            return configByte;
        }

        private int checkHeaderSRAM(byte[] game)
        {
            byte isHirom;
            byte isHirom2;
            byte isLorom;
            byte hasSRAM;
            int configByte = 0;

            if (game.Length != 0)
            {
                isHirom = game[0xffd5];
                isHirom2 = game[0xffd4];
                isLorom = game[0x7fd5];


                if ((isHirom == 0x21 || isHirom == 0x25 || isHirom == 0x31 || isHirom == 0x35) && isHirom2 == 0x20 || isHirom2 == 0x70 || isHirom2 == 0x33) /*fixes a few games*/
                {
                    if (game[0xffd8] != 0x00)
                    {
                        configByte = 1; //has SRAM
                    }
                    else
                    {
                        configByte = 0; //no sram
                    }                    
                }
                else if ((isLorom == 0x20 || isLorom == 0x30))
                {
                    if (game[0x7fd8] != 0x00)
                    {
                        configByte = 1; //has SRAM
                    }
                    else
                    {
                        configByte = 0; //no sram
                    }
                }
                else
                {
                    MessageBox.Show("Mapper type not found. Possibly bad ROM header?");
                }
            }
            else
            {
                MessageBox.Show("Game not found!");
            }
            return configByte;
        }

        private void aboutLink_Click(object sender, EventArgs e)
        {
            string line1 = "                         --SNES MultiCart Maker--";
            string line2 = "                      Version 0.03, July 19, 2022";
            string line3 = "For use with the RetroStage SNES Blaster product.";
            string line4 = "Use your own legally obtained ROM files.";
            string line5 = "We do not condone piracy.";
            MessageBox.Show(line1 + Environment.NewLine + line2 + Environment.NewLine + Environment.NewLine +
                            line3 + Environment.NewLine + Environment.NewLine + line4);// + Environment.NewLine + line5);
        }

        private void first_Click(object sender, EventArgs e)
        {
            int scrubbedLength = 0;
            byte[] gameTemp;

            if (game1Selected == true)
            {
                game1Selected = false;
                currSize = currSize - firstSize;
                //progressBar1.BeginInvoke(new Action(() => { progressBar1.Value = sizeLeft; }));
            }

            if (is8mb)
            {
                maxSize = is8mbflash;
            }
            else
            {
                maxSize = is16mbflash;
            }

            OpenFileDialog firstgame = new OpenFileDialog();
            firstgame.InitialDirectory = "C:/";
            firstgame.Filter = "All files (*.*)|*.*|SNES ROM files |*.sfc; *.smc; *.bin";
            firstgame.FilterIndex = 2;
            firstgame.RestoreDirectory = true;

            if (firstgame.ShowDialog() == DialogResult.OK)
            {
                string filepath = firstgame.FileName;
                snesHeader = File.ReadAllBytes(filepath);
                selectedDirectory = Path.GetDirectoryName(filepath);
                firsttext.Text = firstgame.SafeFileName;
                game1Selected = true;

                if (snesHeader.Length % 1024 == 0)
                {
                    gameTemp = snesHeader;
                }
                else
                {
                    gameTemp = new byte[snesHeader.Length - 512];
                    Buffer.BlockCopy(snesHeader, 512, gameTemp, 0, snesHeader.Length - 512);
                }

                if (gameTemp.Length < 2097152)
                {
                    int check = 2097152 - gameTemp.Length;
                    byte[] blank = { 0xff };
                    scrubbedLength = gameTemp.Length + check;
                }
                else if (gameTemp.Length > 2097152 && gameTemp.Length < 4194304)
                {                    
                    int check = 4194304 - gameTemp.Length;
                    byte[] blank = { 0xff };
                    scrubbedLength = gameTemp.Length + check;
                }
                else
                {
                    scrubbedLength = gameTemp.Length;
                }

                if (gameTemp.Length > 4194304)
                {
                    MessageBox.Show("Game 1 is too large. Max game size is 4MByte for any game slot.");
                    gamesSelected = 1;
                    firsttext.Text = " ";
                    game1Selected = false;
                }
                else
                {
                    currSize = currSize + scrubbedLength;
                    sizeLeft = scrubbedLength;                    
                    maxSize = maxSize - scrubbedLength;
                    gamesSelected = 2;
                    firstSize = scrubbedLength;
                    snes1 = new byte[scrubbedLength];
                    Array.ConstrainedCopy(gameTemp, 0, snes1, 0, gameTemp.Length);
                    progressBar1.BeginInvoke(new Action(() => { progressBar1.Value = currSize; }));
                }
            }
        }

        private void second_Click(object sender, EventArgs e)
        {
            int scrubbedLength = 0;
            byte[] gameTemp;

            if (game2Selected == true)
            {
                game2Selected = false;
                currSize = currSize - secondSize;
                //progressBar1.BeginInvoke(new Action(() => { progressBar1.Value = sizeLeft; }));
            }

            if (gamesSelected >= 2)
            {

                OpenFileDialog secondgame = new OpenFileDialog();
                secondgame.InitialDirectory = selectedDirectory;
                secondgame.Filter = "All files (*.*)|*.*|SNES ROM files |*.sfc; *.smc; *.bin";
                secondgame.FilterIndex = 2;
                secondgame.RestoreDirectory = true;

                if (secondgame.ShowDialog() == DialogResult.OK)
                {
                    string filepath = secondgame.FileName;
                    snesHeader = File.ReadAllBytes(filepath);
                    selectedDirectory = Path.GetDirectoryName(filepath);
                    secondtext.Text = secondgame.SafeFileName;
                    game2Selected = true;

                    if (snesHeader.Length % 1024 == 0)
                    {
                        gameTemp = snesHeader;
                    }
                    else
                    {
                        gameTemp = new byte[snesHeader.Length - 512];
                        Buffer.BlockCopy(snesHeader, 512, gameTemp, 0, snesHeader.Length - 512);
                    }

                    if (gameTemp.Length < 2097152)
                    {
                        int check = 2097152 - gameTemp.Length;
                        byte[] blank = { 0xff };
                        scrubbedLength = gameTemp.Length + check;
                    }
                    else if (gameTemp.Length > 2097152 && gameTemp.Length < 4194304)
                    {
                        int check = 4194304 - gameTemp.Length;
                        byte[] blank = { 0xff };
                        scrubbedLength = gameTemp.Length + check;
                    }
                    else
                    {
                        scrubbedLength = gameTemp.Length;
                    }

                    if (gameTemp.Length > 4194304)
                    {
                        MessageBox.Show("Game 2 is too large. Max game size is 4MByte for any game slot.");
                        gamesSelected = 2;
                        secondtext.Text = " ";
                        game2Selected = false;
                    }
                    else
                    {
                        currSize = currSize + scrubbedLength;
                        sizeLeft = scrubbedLength;
                        progressBar1.BeginInvoke(new Action(() => { progressBar1.Value = currSize; }));
                        maxSize = maxSize - scrubbedLength;
                        gamesSelected = 3;
                        secondSize = scrubbedLength;
                        snes2 = new byte[scrubbedLength];
                        Array.ConstrainedCopy(gameTemp, 0, snes2, 0, gameTemp.Length);

                    }
                }
            }
            else
            {
                MessageBox.Show("You must choose your first game before any others.");
            }
        }

        private void third_Click(object sender, EventArgs e)
        {
            int scrubbedLength = 0;
            byte[] gameTemp;
            bool cancelled = false;

            if (game3Selected == true)
            {
                game3Selected = false;
                currSize = currSize - thirdSize;
                //progressBar1.BeginInvoke(new Action(() => { progressBar1.Value = sizeLeft; }));
            }

            if (gamesSelected >= 3)
            {

                OpenFileDialog thirdgame = new OpenFileDialog();
                thirdgame.InitialDirectory = selectedDirectory;
                thirdgame.Filter = "All files (*.*)|*.*|SNES ROM files |*.sfc; *.smc; *.bin";
                thirdgame.FilterIndex = 2;
                thirdgame.RestoreDirectory = true;

                if (thirdgame.ShowDialog() == DialogResult.OK)
                {
                    string filepath = thirdgame.FileName;
                    snesHeader = File.ReadAllBytes(filepath);
                    selectedDirectory = Path.GetDirectoryName(filepath);
                    thirdtext.Text = thirdgame.SafeFileName;
                    game3Selected = true;

                    if (snesHeader.Length % 1024 == 0)
                    {
                        gameTemp = snesHeader;
                    }
                    else
                    {
                        gameTemp = new byte[snesHeader.Length - 512];
                        Buffer.BlockCopy(snesHeader, 512, gameTemp, 0, snesHeader.Length - 512);
                    }

                    if (is8mb)
                    {
                        if (gameTemp.Length < 2097152)
                        {
                            int check = 2097152 - gameTemp.Length;
                            byte[] blank = { 0xff };
                            scrubbedLength = gameTemp.Length + check;
                        }
                        else if (gameTemp.Length > 2097152)
                        {
                            MessageBox.Show("Cannot add game, as there is not enough space free on the 8MByte flash. When using more than 2 games, they need to be 2MByte or less each.");
                            cancelled = true;
                            gamesSelected = 3;
                            thirdtext.Text = " ";
                            game3Selected = false;
                        }
                        else
                        {
                            scrubbedLength = gameTemp.Length;
                        }
                    }
                    else
                    {
                        if (gameTemp.Length < 2097152)
                        {
                            int check = 2097152 - gameTemp.Length;
                            byte[] blank = { 0xff };
                            scrubbedLength = gameTemp.Length + check;
                        }
                        else if (gameTemp.Length > 2097152)
                        {
                            int check = 4194304 - gameTemp.Length;
                            byte[] blank = { 0xff };
                            scrubbedLength = gameTemp.Length + check;
                        }
                        else
                        {
                            scrubbedLength = gameTemp.Length;
                        }
                    }

                    if (!cancelled)
                    {
                        if (gameTemp.Length > 4194304)
                        {
                            MessageBox.Show("Game 3 is too large. Max game size is 4MByte for any game slot.");
                            gamesSelected = 3;
                            thirdtext.Text = " ";
                            game3Selected = false;
                        }
                        else
                        {
                            currSize = currSize + scrubbedLength;
                            sizeLeft = scrubbedLength;
                            progressBar1.BeginInvoke(new Action(() => { progressBar1.Value = currSize; }));
                            maxSize = maxSize - scrubbedLength;
                            gamesSelected = 4;
                            thirdSize = scrubbedLength;
                            snes3 = new byte[scrubbedLength];
                            Array.ConstrainedCopy(gameTemp, 0, snes3, 0, gameTemp.Length);

                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("You must choose your first and second game before any others.");
            }
        }

        private void fourth_Click(object sender, EventArgs e)
        {
            int scrubbedLength = 0;
            byte[] gameTemp;
            bool cancelled = false;

            if (game4Selected == true)
            {
                game4Selected = false;
                currSize = currSize - fourthSize;
                //progressBar1.BeginInvoke(new Action(() => { progressBar1.Value = sizeLeft; }));
            }

            if (gamesSelected >= 4)
            {

                OpenFileDialog fourthgame = new OpenFileDialog();
                fourthgame.InitialDirectory = selectedDirectory;
                fourthgame.Filter = "All files (*.*)|*.*|SNES ROM files |*.sfc; *.smc; *.bin";
                fourthgame.FilterIndex = 2;
                fourthgame.RestoreDirectory = true;

                if (fourthgame.ShowDialog() == DialogResult.OK)
                {
                    string filepath = fourthgame.FileName;
                    snesHeader = File.ReadAllBytes(filepath);
                    selectedDirectory = Path.GetDirectoryName(filepath);
                    fourthtext.Text = fourthgame.SafeFileName;
                    game4Selected = true;

                    if (snesHeader.Length % 1024 == 0)
                    {
                        gameTemp = snesHeader;
                    }
                    else
                    {
                        gameTemp = new byte[snesHeader.Length - 512];
                        Buffer.BlockCopy(snesHeader, 512, gameTemp, 0, snesHeader.Length - 512);
                    }

                    if (is8mb)
                    {
                        if (gameTemp.Length < 2097152)
                        {
                            int check = 2097152 - gameTemp.Length;
                            byte[] blank = { 0xff };
                            scrubbedLength = gameTemp.Length + check;
                        }
                        else if (gameTemp.Length > 2097152)
                        {
                            MessageBox.Show("Cannot add game, as there is not enough space free on the 8MByte flash. When using more than 2 games, they need to be 2MByte or less each.");
                            cancelled = true;
                            gamesSelected = 4;
                            fourthtext.Text = " ";
                            game4Selected = false;
                        }
                        else
                        {
                            scrubbedLength = gameTemp.Length;
                        }
                    }
                    else
                    {
                        if (gameTemp.Length < 2097152)
                        {
                            int check = 2097152 - gameTemp.Length;
                            byte[] blank = { 0xff };
                            scrubbedLength = gameTemp.Length + check;
                        }
                        else if (gameTemp.Length > 2097152)
                        {
                            int check = 4194304 - gameTemp.Length;
                            byte[] blank = { 0xff };
                            scrubbedLength = gameTemp.Length + check;
                        }
                        else
                        {
                            scrubbedLength = gameTemp.Length;
                        }
                    }

                    if (!cancelled)
                    {
                        if (gameTemp.Length > 4194304)
                        {
                            MessageBox.Show("Game 4 is too large. Max game size is 4MByte for any game slot.");
                            gamesSelected = 4;
                            fourthtext.Text = " ";
                            game4Selected = false;
                        }
                        else
                        {
                            currSize = currSize + scrubbedLength;
                            sizeLeft = scrubbedLength;
                            progressBar1.BeginInvoke(new Action(() => { progressBar1.Value = currSize; }));
                            maxSize = maxSize - scrubbedLength;
                            gamesSelected = 5;
                            fourthSize = scrubbedLength;
                            snes4 = new byte[scrubbedLength];
                            Array.ConstrainedCopy(gameTemp, 0, snes4, 0, gameTemp.Length);

                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("You must choose your first, second and third game before the fourth.");
            }
        }

        private void eightmb_CheckedChanged(object sender, EventArgs e)
        {
            if(eightmb.Checked == true)
            {
                capacityChanged(8);
            }
        }

        private void sixteenmb_CheckedChanged(object sender, EventArgs e)
        {
            if (sixteenmb.Checked == true)
            {
                capacityChanged(16);
            }
        }
    }
}
