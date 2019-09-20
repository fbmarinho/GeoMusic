using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GeoMusic.Properties;
using GeoMusic.Tools;
using Sanford.Multimedia.Midi;
using System.Drawing;

namespace GeoMusic
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.openMidiDialog = new System.Windows.Forms.OpenFileDialog();
            this.LoadMidiBtn = new System.Windows.Forms.Button();
            this.CarregarInstrumentosBtn = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.AppWatch = new System.Windows.Forms.Timer(this.components);
            this.MidiPlayBtn = new System.Windows.Forms.Button();
            this.InstrumentoPreview = new System.Windows.Forms.PictureBox();
            this.OutputFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.sequence = new Sanford.Multimedia.Midi.Sequence();
            this.sequencer = new Sanford.Multimedia.Midi.Sequencer();
            this.MidiStopBtn = new System.Windows.Forms.Button();
            this.FullScreenBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tfDelay = new System.Windows.Forms.NumericUpDown();
            this.DelayTimer = new System.Windows.Forms.Timer(this.components);
            this.DelayLabel = new System.Windows.Forms.Label();
            this.NoteOffCb = new System.Windows.Forms.CheckBox();
            this.DeviceSelect = new System.Windows.Forms.ComboBox();
            this.labelTransposicao = new System.Windows.Forms.Label();
            this.TransSemitones = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.InstrumentoPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tfDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // openMidiDialog
            // 
            this.openMidiDialog.FileName = "MIDI Files";
            this.openMidiDialog.Filter = "Arquivos MIDI (*.midi,*.mid)|*.midi;*.mid|Todos os Arquivos (*.*)|*.*";
            this.openMidiDialog.InitialDirectory = "C:\\Users\\LEENER\\Music";
            // 
            // LoadMidiBtn
            // 
            this.LoadMidiBtn.Enabled = false;
            this.LoadMidiBtn.Location = new System.Drawing.Point(12, 38);
            this.LoadMidiBtn.Name = "LoadMidiBtn";
            this.LoadMidiBtn.Size = new System.Drawing.Size(249, 23);
            this.LoadMidiBtn.TabIndex = 0;
            this.LoadMidiBtn.Text = "&Carregar arquivo Midi";
            this.LoadMidiBtn.UseVisualStyleBackColor = true;
            this.LoadMidiBtn.Click += new System.EventHandler(this.LoadMidiBtn_Click);
            // 
            // CarregarInstrumentosBtn
            // 
            this.CarregarInstrumentosBtn.Location = new System.Drawing.Point(12, 67);
            this.CarregarInstrumentosBtn.Name = "CarregarInstrumentosBtn";
            this.CarregarInstrumentosBtn.Size = new System.Drawing.Size(168, 24);
            this.CarregarInstrumentosBtn.TabIndex = 0;
            this.CarregarInstrumentosBtn.Text = "Carregar Instrumento";
            this.CarregarInstrumentosBtn.Click += new System.EventHandler(this.CarregarInstrumentosBtn_Click);
            // 
            // AppWatch
            // 
            this.AppWatch.Enabled = true;
            this.AppWatch.Tick += new System.EventHandler(this.Watch_Tick);
            // 
            // MidiPlayBtn
            // 
            this.MidiPlayBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MidiPlayBtn.Enabled = false;
            this.MidiPlayBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MidiPlayBtn.Location = new System.Drawing.Point(13, 301);
            this.MidiPlayBtn.Name = "MidiPlayBtn";
            this.MidiPlayBtn.Size = new System.Drawing.Size(86, 23);
            this.MidiPlayBtn.TabIndex = 5;
            this.MidiPlayBtn.Text = "Preview";
            this.MidiPlayBtn.UseVisualStyleBackColor = true;
            this.MidiPlayBtn.Click += new System.EventHandler(this.MidiPlayBtn_Click);
            // 
            // InstrumentoPreview
            // 
            this.InstrumentoPreview.InitialImage = null;
            this.InstrumentoPreview.Location = new System.Drawing.Point(12, 128);
            this.InstrumentoPreview.Name = "InstrumentoPreview";
            this.InstrumentoPreview.Size = new System.Drawing.Size(247, 165);
            this.InstrumentoPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.InstrumentoPreview.TabIndex = 6;
            this.InstrumentoPreview.TabStop = false;
            // 
            // OutputFileDialog
            // 
            this.OutputFileDialog.Filter = "Arquivos WMV (*.wmv)|*.wmv";
            // 
            // sequence
            // 
            this.sequence.Format = 1;
            // 
            // sequencer
            // 
            this.sequencer.Position = 0;
            this.sequencer.Sequence = this.sequence;
            this.sequencer.PlayingCompleted += new System.EventHandler(this.HandlePlayingCompleted);
            this.sequencer.ChannelMessagePlayed += new System.EventHandler<Sanford.Multimedia.Midi.ChannelMessageEventArgs>(this.HandleChannelMessagePlayed);
            this.sequencer.SysExMessagePlayed += new System.EventHandler<Sanford.Multimedia.Midi.SysExMessageEventArgs>(this.HandleSysExMessagePlayed);
            this.sequencer.Chased += new System.EventHandler<Sanford.Multimedia.Midi.ChasedEventArgs>(this.HandleChased);
            this.sequencer.Stopped += new System.EventHandler<Sanford.Multimedia.Midi.StoppedEventArgs>(this.HandleStopped);
            // 
            // MidiStopBtn
            // 
            this.MidiStopBtn.Location = new System.Drawing.Point(14, 302);
            this.MidiStopBtn.Name = "MidiStopBtn";
            this.MidiStopBtn.Size = new System.Drawing.Size(86, 23);
            this.MidiStopBtn.TabIndex = 12;
            this.MidiStopBtn.Text = "Stop";
            this.MidiStopBtn.UseVisualStyleBackColor = true;
            this.MidiStopBtn.Click += new System.EventHandler(this.MidiStopBtn_Click);
            // 
            // FullScreenBtn
            // 
            this.FullScreenBtn.Enabled = false;
            this.FullScreenBtn.Location = new System.Drawing.Point(184, 302);
            this.FullScreenBtn.Name = "FullScreenBtn";
            this.FullScreenBtn.Size = new System.Drawing.Size(77, 23);
            this.FullScreenBtn.TabIndex = 13;
            this.FullScreenBtn.Text = "Tela Cheia";
            this.FullScreenBtn.UseVisualStyleBackColor = true;
            this.FullScreenBtn.Click += new System.EventHandler(this.FullScreen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 307);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Delay";
            // 
            // tfDelay
            // 
            this.tfDelay.Location = new System.Drawing.Point(138, 303);
            this.tfDelay.Name = "tfDelay";
            this.tfDelay.Size = new System.Drawing.Size(38, 20);
            this.tfDelay.TabIndex = 16;
            this.tfDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tfDelay.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.tfDelay.ValueChanged += new System.EventHandler(this.tfDelay_ValueChanged);
            // 
            // DelayTimer
            // 
            this.DelayTimer.Interval = 1000;
            this.DelayTimer.Tick += new System.EventHandler(this.DelayTimer_Tick);
            // 
            // DelayLabel
            // 
            this.DelayLabel.AutoSize = true;
            this.DelayLabel.BackColor = System.Drawing.Color.Black;
            this.DelayLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DelayLabel.Cursor = System.Windows.Forms.Cursors.No;
            this.DelayLabel.Enabled = false;
            this.DelayLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.DelayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 80F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DelayLabel.ForeColor = System.Drawing.Color.White;
            this.DelayLabel.Location = new System.Drawing.Point(48, 148);
            this.DelayLabel.Name = "DelayLabel";
            this.DelayLabel.Size = new System.Drawing.Size(172, 122);
            this.DelayLabel.TabIndex = 17;
            this.DelayLabel.Text = "99";
            this.DelayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DelayLabel.Visible = false;
            // 
            // NoteOffCb
            // 
            this.NoteOffCb.AutoSize = true;
            this.NoteOffCb.Location = new System.Drawing.Point(187, 72);
            this.NoteOffCb.Name = "NoteOffCb";
            this.NoteOffCb.Size = new System.Drawing.Size(73, 17);
            this.NoteOffCb.TabIndex = 18;
            this.NoteOffCb.Text = "Note off ?";
            this.NoteOffCb.UseVisualStyleBackColor = true;
            // 
            // DeviceSelect
            // 
            this.DeviceSelect.FormattingEnabled = true;
            this.DeviceSelect.Location = new System.Drawing.Point(14, 9);
            this.DeviceSelect.Name = "DeviceSelect";
            this.DeviceSelect.Size = new System.Drawing.Size(247, 21);
            this.DeviceSelect.TabIndex = 19;
            this.DeviceSelect.Text = "Selecione o Dispositivo";
            this.DeviceSelect.SelectedIndexChanged += new System.EventHandler(this.DeviceSelect_SelectedIndexChanged);
            // 
            // labelTransposicao
            // 
            this.labelTransposicao.AutoSize = true;
            this.labelTransposicao.Location = new System.Drawing.Point(12, 102);
            this.labelTransposicao.Name = "labelTransposicao";
            this.labelTransposicao.Size = new System.Drawing.Size(114, 13);
            this.labelTransposicao.TabIndex = 20;
            this.labelTransposicao.Text = "Fazer transposição de:";
            // 
            // TransSemitones
            // 
            this.TransSemitones.FormattingEnabled = true;
            this.TransSemitones.Location = new System.Drawing.Point(132, 99);
            this.TransSemitones.Name = "TransSemitones";
            this.TransSemitones.Size = new System.Drawing.Size(127, 21);
            this.TransSemitones.TabIndex = 22;
            this.TransSemitones.SelectedIndexChanged += new System.EventHandler(this.TransSemitones_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 339);
            this.Controls.Add(this.TransSemitones);
            this.Controls.Add(this.labelTransposicao);
            this.Controls.Add(this.DelayLabel);
            this.Controls.Add(this.InstrumentoPreview);
            this.Controls.Add(this.DeviceSelect);
            this.Controls.Add(this.NoteOffCb);
            this.Controls.Add(this.tfDelay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FullScreenBtn);
            this.Controls.Add(this.MidiStopBtn);
            this.Controls.Add(this.MidiPlayBtn);
            this.Controls.Add(this.CarregarInstrumentosBtn);
            this.Controls.Add(this.LoadMidiBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "GeoMusic 2.5";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.InstrumentoPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tfDelay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openMidiDialog;
        private System.Windows.Forms.Button LoadMidiBtn;
        private Button CarregarInstrumentosBtn;
        private FolderBrowserDialog folderBrowserDialog;
        private Timer AppWatch;
        private Button MidiPlayBtn;
        private PictureBox InstrumentoPreview;
        private SaveFileDialog OutputFileDialog;

        private Sequence sequence;
        private Sequencer sequencer;
        private Button MidiStopBtn;
        private Button FullScreenBtn;
        private Label label1;
        private NumericUpDown tfDelay;
        private Timer DelayTimer;
        private Label DelayLabel;
        private Stream Resource;
        private Bitmap Cover;
        private CheckBox NoteOffCb;
        private ComboBox DeviceSelect;
        private Label labelTransposicao;
        private ComboBox TransSemitones;
    }
 }