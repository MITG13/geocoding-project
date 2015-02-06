namespace Coder
{
    partial class FHWN_GeoCoder
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
            this.txt_address = new System.Windows.Forms.TextBox();
            this.lbl_about = new System.Windows.Forms.Label();
            this.cmb_geocoder = new System.Windows.Forms.ComboBox();
            this.btn_geocode = new System.Windows.Forms.Button();
            this.lbl_result = new System.Windows.Forms.Label();
            this.lbl_lat = new System.Windows.Forms.Label();
            this.lbl_lng = new System.Windows.Forms.Label();
            this.txt_lat = new System.Windows.Forms.TextBox();
            this.txt_lng = new System.Windows.Forms.TextBox();
            this.pic_logo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_address
            // 
            this.txt_address.Location = new System.Drawing.Point(15, 48);
            this.txt_address.Name = "txt_address";
            this.txt_address.Size = new System.Drawing.Size(272, 20);
            this.txt_address.TabIndex = 0;
            // 
            // lbl_about
            // 
            this.lbl_about.AutoSize = true;
            this.lbl_about.Location = new System.Drawing.Point(15, 21);
            this.lbl_about.Name = "lbl_about";
            this.lbl_about.Size = new System.Drawing.Size(48, 13);
            this.lbl_about.TabIndex = 1;
            this.lbl_about.Text = "Address:";
            // 
            // cmb_geocoder
            // 
            this.cmb_geocoder.FormattingEnabled = true;
            this.cmb_geocoder.Location = new System.Drawing.Point(15, 84);
            this.cmb_geocoder.Name = "cmb_geocoder";
            this.cmb_geocoder.Size = new System.Drawing.Size(121, 21);
            this.cmb_geocoder.TabIndex = 2;
            this.cmb_geocoder.Text = "Choose Geocoder";
            // 
            // btn_geocode
            // 
            this.btn_geocode.Location = new System.Drawing.Point(15, 121);
            this.btn_geocode.Name = "btn_geocode";
            this.btn_geocode.Size = new System.Drawing.Size(121, 23);
            this.btn_geocode.TabIndex = 3;
            this.btn_geocode.Text = "Geocode Address";
            this.btn_geocode.UseVisualStyleBackColor = true;
            this.btn_geocode.Click += new System.EventHandler(this.btn_geocode_Click);
            // 
            // lbl_result
            // 
            this.lbl_result.AutoSize = true;
            this.lbl_result.Location = new System.Drawing.Point(15, 188);
            this.lbl_result.Name = "lbl_result";
            this.lbl_result.Size = new System.Drawing.Size(40, 13);
            this.lbl_result.TabIndex = 4;
            this.lbl_result.Text = "Result:";
            // 
            // lbl_lat
            // 
            this.lbl_lat.AutoSize = true;
            this.lbl_lat.Location = new System.Drawing.Point(15, 220);
            this.lbl_lat.Name = "lbl_lat";
            this.lbl_lat.Size = new System.Drawing.Size(48, 13);
            this.lbl_lat.TabIndex = 5;
            this.lbl_lat.Text = "Latitude:";
            // 
            // lbl_lng
            // 
            this.lbl_lng.AutoSize = true;
            this.lbl_lng.Location = new System.Drawing.Point(15, 253);
            this.lbl_lng.Name = "lbl_lng";
            this.lbl_lng.Size = new System.Drawing.Size(57, 13);
            this.lbl_lng.TabIndex = 6;
            this.lbl_lng.Text = "Longitude:";
            // 
            // txt_lat
            // 
            this.txt_lat.Location = new System.Drawing.Point(70, 216);
            this.txt_lat.Name = "txt_lat";
            this.txt_lat.Size = new System.Drawing.Size(217, 20);
            this.txt_lat.TabIndex = 7;
            // 
            // txt_lng
            // 
            this.txt_lng.Location = new System.Drawing.Point(70, 250);
            this.txt_lng.Name = "txt_lng";
            this.txt_lng.Size = new System.Drawing.Size(217, 20);
            this.txt_lng.TabIndex = 8;
            // 
            // pic_logo
            // 
            this.pic_logo.Image = global::Coder.Properties.Resources.icon_sm;
            this.pic_logo.Location = new System.Drawing.Point(168, 84);
            this.pic_logo.MaximumSize = new System.Drawing.Size(50, 50);
            this.pic_logo.MinimumSize = new System.Drawing.Size(100, 100);
            this.pic_logo.Name = "pic_logo";
            this.pic_logo.Size = new System.Drawing.Size(100, 100);
            this.pic_logo.TabIndex = 9;
            this.pic_logo.TabStop = false;
            // 
            // FHWN_GeoCoder
            // 
            this.Controls.Add(this.pic_logo);
            this.Controls.Add(this.txt_lng);
            this.Controls.Add(this.txt_lat);
            this.Controls.Add(this.lbl_lng);
            this.Controls.Add(this.lbl_lat);
            this.Controls.Add(this.lbl_result);
            this.Controls.Add(this.btn_geocode);
            this.Controls.Add(this.cmb_geocoder);
            this.Controls.Add(this.lbl_about);
            this.Controls.Add(this.txt_address);
            this.Name = "FHWN_GeoCoder";
            this.Size = new System.Drawing.Size(300, 297);
            this.Load += new System.EventHandler(this.dockWindow_load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_address;
        private System.Windows.Forms.Label lbl_about;
        private System.Windows.Forms.ComboBox cmb_geocoder;
        private System.Windows.Forms.Button btn_geocode;
        private System.Windows.Forms.Label lbl_result;
        private System.Windows.Forms.Label lbl_lat;
        private System.Windows.Forms.Label lbl_lng;
        private System.Windows.Forms.TextBox txt_lat;
        private System.Windows.Forms.TextBox txt_lng;
        private System.Windows.Forms.PictureBox pic_logo;

    }
}
