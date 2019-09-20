using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GeoMusic.Tools;
using Sanford.Multimedia.Midi;

namespace GeoMusic
{
    public partial class MainForm : Form
    {
        public bool MidiLoaded { get; set; }
        public bool InstrumentLoaded { get; set; }
        public bool playing;
        public string InstrumentPath;
        public int delay;
        public Bitmap[] Instrument;

        private OutputDevice outDevice;
        private int outDeviceID = 0;
        private int transpose = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        public Bitmap GetImageResource(string name)
        {
            var file = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames().FirstOrDefault(q=>q.Contains(name));
            Stream Resource = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(file);
            return new Bitmap(Resource);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            InstrumentoPreview.Image = GetImageResource("Cover");

            MidiLoaded = false;
            InstrumentLoaded = false;
            playing = false;

            //Preenche Combobox de transposição
            for (int i=-24;i<=24;i++)
            {
                TransSemitones.Items.Add(String.Concat(i.ToString(), " semitons"));
            }
            TransSemitones.SelectedIndex = 24;


            //Carregando Dispositivos MIDI
            if (OutputDevice.DeviceCount > 0)
            {
                try
                {
                    List<string> devices = new List<string>();
                    for (int i = 0; i < OutputDevice.DeviceCount; i++)
                    {
                        devices.Add(OutputDevice.GetDeviceCapabilities(i).name);
                    }

                    DeviceSelect.Items.AddRange(devices.ToArray());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("Não foram encontrados dispositivos de saída neste computador. \n Não será possível reproduzir sons...", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadMidiBtn_Click(object sender, EventArgs e)
        {

            DialogResult result = openMidiDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                //Load Midi file in memory
                sequence.LoadAsync(openMidiDialog.FileName);
            }
            Console.WriteLine(result); // <-- For debugging use.
        }

        private void CarregarInstrumentosBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                InstrumentPath = folderBrowserDialog.SelectedPath;

                try
                {
                    Instrument = FileProcessor.LoadInstrument(InstrumentPath);

                    CarregarInstrumentosBtn.Text = InstrumentPath.Split('\\').Last();
                    InstrumentoPreview.Image = Instrument[0];
                    InstrumentLoaded = true;
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Ocorreu um erro ao carregar o instrumento...");
                    //throw;
                }
                
            }
            else
            {
                InstrumentLoaded = false;
            }
            Console.WriteLine(result); // <-- For debugging use.

        }


        private void Watch_Tick(object sender, EventArgs e)
        {
            //ProgressBar.Value = sequencer.Position;

            MidiPlayBtn.Visible = !playing;
            MidiStopBtn.Visible = playing;

            if (MidiLoaded)
            {
                MidiPlayBtn.Enabled = !playing;
                MidiStopBtn.Enabled = playing;
            }

            if (MidiLoaded && InstrumentLoaded)
            {
                FullScreenBtn.Enabled = true;
            }


        }

        private void MidiPlayBtn_Click(object sender, EventArgs e)
        {

            StartPlaying();
        }

        private void MidiStopBtn_Click(object sender, EventArgs e)
        {
            StopPlaying();
        }


        //Handlers
        private void HandleLoadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //ProgressBar.Value = e.ProgressPercentage;
        }

        private void HandleLoadCompleted(object sender, AsyncCompletedEventArgs e)
        {

            if (e.Error == null)
            {
                MidiLoaded = true;
                LoadMidiBtn.Text = openMidiDialog.SafeFileName;
                Console.WriteLine("Carregou arquivo...");
            }
            else
            {
                LoadMidiBtn.Text = "Erro no arquivo";
                MessageBox.Show(e.Error.Message);
            }
        }

        private void HandleChannelMessagePlayed(object sender, ChannelMessageEventArgs e)
        {

            if (e.Message.Command == ChannelCommand.NoteOn)
            {
                int tone = (e.Message.Data1 + transpose);
                Console.WriteLine(tone.ToString());

                if (InstrumentLoaded)
                {
                    Console.WriteLine(e.Message.Data1.ToString());

                    InstrumentoPreview.Image = Instrument[tone];
                }
                else
                {
                    InstrumentoPreview.Image = GetImageResource(tone.ToString());
                }
            }
            if (NoteOffCb.Checked)
            {

                if (e.Message.Command == ChannelCommand.NoteOff)
                {
                    if (InstrumentLoaded)
                    {
                        InstrumentoPreview.Image = Instrument[1];
                    }
                    else
                    {
                        InstrumentoPreview.Image = GetImageResource("Off");
                    }
                    
                }
            }


            outDevice.Send(e.Message);
        }

        private void HandleChased(object sender, ChasedEventArgs e)
        {
            foreach (ChannelMessage message in e.Messages)
            {
                outDevice.Send(message);
            }
        }

        private void HandleSysExMessagePlayed(object sender, SysExMessageEventArgs e)
        {
            //     outDevice.Send(e.Message); Sometimes causes an exception to be thrown because the output device is overloaded.
        }

        private void HandleStopped(object sender, StoppedEventArgs e)
        {
            foreach (ChannelMessage message in e.Messages)
            {
                outDevice.Send(message);
            }
        }

        private void HandlePlayingCompleted(object sender, EventArgs e)
        {
            playing = false;
        }

        private void FullScreen_Click(object sender, EventArgs e)
        {
            StopPlaying();
            delay = (int)tfDelay.Value;
            InstrumentoPreview.Dock = DockStyle.Fill;
            InstrumentoPreview.Location = new Point((Width - InstrumentoPreview.Width) / 2, (Height - InstrumentoPreview.Height) / 2);
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            DelayLabel.Text = tfDelay.Text;
            DelayLabel.Visible = true;
            DelayTimer.Start();
            labelTransposicao.Visible = false;
            TransSemitones.Visible = false;
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                InstrumentoPreview.Dock = DockStyle.None;
                InstrumentoPreview.Location = new Point(12, 128);
                FormBorderStyle = FormBorderStyle.FixedDialog;
                WindowState = FormWindowState.Normal;
                DelayLabel.Visible = false;
                DelayTimer.Stop();
                labelTransposicao.Visible = true;
                TransSemitones.Visible = true;
                StopPlaying();
            }

        }

        private void DelayTimer_Tick(object sender, EventArgs e)
        {
            if (!playing)
            {
                if (delay > 0)
                {
                    DelayLabel.Text = delay.ToString();
                    delay--;
                }
                else
                {
                    StartPlaying();
                }
            }

        }

        private void tfDelay_ValueChanged(object sender, EventArgs e)
        {
            DelayLabel.Text = tfDelay.Value.ToString();
        }


        private void StartPlaying()
        {
            DelayLabel.Visible = false;
            try
            {
                sequencer.Start();
                playing = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void StopPlaying()
        {
            try
            {
                sequencer.Stop();
                if (InstrumentLoaded)
                {
                    InstrumentoPreview.Image = Instrument[0];
                }
                playing = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopPlaying();
            Application.Exit();
            Environment.Exit(0);
        }

        private void DeviceSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (outDevice != null)
                {
                    StopPlaying();
                    outDevice.Close();
                    outDevice.Dispose();
                }
                LoadMidiBtn.Enabled = true;
                outDevice = new OutputDevice(DeviceSelect.SelectedIndex);
                sequence.LoadProgressChanged += HandleLoadProgressChanged;
                sequence.LoadCompleted += HandleLoadCompleted;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            
        }

        private void TransSemitones_SelectedIndexChanged(object sender, EventArgs e)
        {
            transpose = TransSemitones.SelectedIndex - 24;
            Console.WriteLine(transpose.ToString());
        }
    }
}
