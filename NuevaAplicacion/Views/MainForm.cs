using NuevaAplicacion.Controllers;

using System.Windows.Forms; // Asegúrate de que esta línea esté presente
using System.Drawing; // Asegúrate de incluir esta directiva


using NuevaAplicacion.Models;

namespace NuevaAplicacion.Views
{
    public partial class MainForm : Form
    {
        private TicketController _controller;

        // Controles de la interfaz
        private DataGridView dgvTickets;
        private TextBox txtTitle, txtDescription, txtAssignedTo;
        private ComboBox cmbPriority;
        private Label lblCurrentState, lblCurrentTicket, lblTicketInfo;
        private Button btnCreate, btnUpdate, btnNext, btnPrevious, btnAdvanceState, btnRegressState, btnClear;
        private ListBox lstHistory;
        private Panel panelMain, panelTop, panelNavigation, panelDetails, panelForm, panelHistory;
        private GroupBox grpTicketForm, grpNavigation, grpHistory, grpActions;

        public MainForm()
        {
            _controller = new TicketController();
            InitializeComponent();
            LoadTickets();
            UpdateNavigationInfo();
            UpdateCurrentTicketDisplay();
        }

        private void InitializeComponent()
        {
            this.Text = "Sistema de Tickets - Soporte IT Empresarial";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1000, 600);
            this.Icon = SystemIcons.Application;

            // Configuración de colores y estilos
            this.BackColor = Color.FromArgb(240, 240, 240);

            InitializePanels();
            InitializeTicketGrid();
            InitializeNavigationControls();
            InitializeFormControls();
            InitializeHistoryControls();
            InitializeActionButtons();
        }

        private void InitializePanels()
        {
            // Panel principal
            panelMain = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(240, 240, 240)
            };
            this.Controls.Add(panelMain);

            // Panel superior - Lista de tickets
            panelTop = new Panel
            {
                Height = 250,
                Dock = DockStyle.Top,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            panelMain.Controls.Add(panelTop);

            // Panel de navegación
            panelNavigation = new Panel
            {
                Height = 80,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(250, 250, 250),
                BorderStyle = BorderStyle.FixedSingle
            };
            panelMain.Controls.Add(panelNavigation);

            // Panel de detalles
            panelDetails = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 245, 245)
            };
            panelMain.Controls.Add(panelDetails);

            // Panel del formulario
            panelForm = new Panel
            {
                Width = 450,
                Dock = DockStyle.Left,
                Padding = new Padding(5),
                BackColor = Color.White
            };
            panelDetails.Controls.Add(panelForm);

            // Panel del historial
            panelHistory = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(5),
                BackColor = Color.White
            };
            panelDetails.Controls.Add(panelHistory);
        }

        private void InitializeTicketGrid()
        {
            var lblTickets = new Label
            {
                Text = "📋 Lista de Tickets",
                Location = new Point(10, 10),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                AutoSize = true
            };
            panelTop.Controls.Add(lblTickets);

            dgvTickets = new DataGridView
            {
                Location = new Point(10, 40),
                Size = new Size(panelTop.Width - 25, 195),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(70, 130, 180),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    Font = new Font("Segoe UI", 9),
                    SelectionBackColor = Color.FromArgb(173, 216, 230),
                    SelectionForeColor = Color.Black
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(248, 248, 248)
                }
            };
            dgvTickets.SelectionChanged += DgvTickets_SelectionChanged;
            panelTop.Controls.Add(dgvTickets);
        }

        private void InitializeNavigationControls()
        {
            grpNavigation = new GroupBox
            {
                Text = "🔍 Navegación de Tickets",
                Location = new Point(10, 10),
                Size = new Size(panelNavigation.Width - 20, 60),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51)
            };
            panelNavigation.Controls.Add(grpNavigation);

            lblCurrentTicket = new Label
            {
                Location = new Point(10, 20),
                Size = new Size(400, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(51, 51, 51)
            };
            grpNavigation.Controls.Add(lblCurrentTicket);

            btnPrevious = new Button
            {
                Text = "⬅ Anterior",
                Location = new Point(420, 18),
                Size = new Size(90, 30),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnPrevious.FlatAppearance.BorderSize = 0;
            btnPrevious.Click += BtnPrevious_Click;
            grpNavigation.Controls.Add(btnPrevious);

            btnNext = new Button
            {
                Text = "Siguiente ➡",
                Location = new Point(520, 18),
                Size = new Size(90, 30),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.Click += BtnNext_Click;
            grpNavigation.Controls.Add(btnNext);
        }

        private void InitializeFormControls()
        {
            grpTicketForm = new GroupBox
            {
                Text = "📝 Información del Ticket",
                Location = new Point(10, 10),
                Size = new Size(430, 350),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51)
            };
            panelForm.Controls.Add(grpTicketForm);

            // Título
            var lblTitle = new Label
            {
                Text = "Título:",
                Location = new Point(15, 30),
                Font = new Font("Segoe UI", 9),
                AutoSize = true
            };
            grpTicketForm.Controls.Add(lblTitle);

            txtTitle = new TextBox
            {
                Location = new Point(15, 50),
                Size = new Size(400, 23),
                Font = new Font("Segoe UI", 9),
                BorderStyle = BorderStyle.FixedSingle
            };
            grpTicketForm.Controls.Add(txtTitle);

            // Descripción
            var lblDescription = new Label
            {
                Text = "Descripción:",
                Location = new Point(15, 85),
                Font = new Font("Segoe UI", 9),
                AutoSize = true
            };
            grpTicketForm.Controls.Add(lblDescription);

            txtDescription = new TextBox
            {
                Location = new Point(15, 105),
                Size = new Size(400, 60),
                Font = new Font("Segoe UI", 9),
                Multiline = true,
                BorderStyle = BorderStyle.FixedSingle,
                ScrollBars = ScrollBars.Vertical
            };
            grpTicketForm.Controls.Add(txtDescription);

            // Asignado a
            var lblAssigned = new Label
            {
                Text = "Asignado a:",
                Location = new Point(15, 180),
                Font = new Font("Segoe UI", 9),
                AutoSize = true
            };
            grpTicketForm.Controls.Add(lblAssigned);

            txtAssignedTo = new TextBox
            {
                Location = new Point(15, 200),
                Size = new Size(400, 23),
                Font = new Font("Segoe UI", 9),
                BorderStyle = BorderStyle.FixedSingle
            };
            grpTicketForm.Controls.Add(txtAssignedTo);

            // Prioridad
            var lblPriority = new Label
            {
                Text = "Prioridad:",
                Location = new Point(15, 235),
                Font = new Font("Segoe UI", 9),
                AutoSize = true
            };
            grpTicketForm.Controls.Add(lblPriority);

            cmbPriority = new ComboBox
            {
                Location = new Point(15, 255),
                Size = new Size(200, 23),
                Font = new Font("Segoe UI", 9),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbPriority.Items.AddRange(new[] { "baja", "media", "alta", "critica" });
            grpTicketForm.Controls.Add(cmbPriority);

            // Estado actual
            lblCurrentState = new Label
            {
                Location = new Point(15, 290),
                Size = new Size(400, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 167, 69)
            };
            grpTicketForm.Controls.Add(lblCurrentState);

            // Información adicional
            lblTicketInfo = new Label
            {
                Location = new Point(15, 315),
                Size = new Size(400, 20),
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(108, 117, 125)
            };
            grpTicketForm.Controls.Add(lblTicketInfo);
        }

        private void InitializeHistoryControls()
        {
            grpHistory = new GroupBox
            {
                Text = "📋 Historial del Ticket",
                Location = new Point(10, 10),
                Size = new Size(panelHistory.Width - 25, 350),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51)
            };
            panelHistory.Controls.Add(grpHistory);

            lstHistory = new ListBox
            {
                Location = new Point(15, 25),
                Size = new Size(grpHistory.Width - 35, 310),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Font = new Font("Consolas", 8),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(248, 249, 250),
                ForeColor = Color.FromArgb(51, 51, 51)
            };
            grpHistory.Controls.Add(lstHistory);
        }

        private void InitializeActionButtons()
        {
            grpActions = new GroupBox
            {
                Text = "⚡ Acciones",
                Location = new Point(10, 370),
                Size = new Size(430, 120),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51)
            };
            panelForm.Controls.Add(grpActions);

            // Primera fila de botones
            btnCreate = new Button
            {
                Text = "➕ Crear Ticket",
                Location = new Point(15, 25),
                Size = new Size(120, 35),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.Click += BtnCreate_Click;
            grpActions.Controls.Add(btnCreate);

            btnUpdate = new Button
            {
                Text = "✏ Actualizar",
                Location = new Point(145, 25),
                Size = new Size(120, 35),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(255, 193, 7),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.Click += BtnUpdate_Click;
            grpActions.Controls.Add(btnUpdate);

            btnClear = new Button
            {
                Text = "🗑 Limpiar",
                Location = new Point(275, 25),
                Size = new Size(120, 35),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Click += BtnClear_Click;
            grpActions.Controls.Add(btnClear);

            // Segunda fila de botones
            btnAdvanceState = new Button
            {
                Text = "⏭ Avanzar Estado",
                Location = new Point(15, 70),
                Size = new Size(180, 35),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(23, 162, 184),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAdvanceState.FlatAppearance.BorderSize = 0;
            btnAdvanceState.Click += BtnAdvanceState_Click;
            grpActions.Controls.Add(btnAdvanceState);

            btnRegressState = new Button
            {
                Text = "⏮ Retroceder Estado",
                Location = new Point(205, 70),
                Size = new Size(180, 35),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRegressState.FlatAppearance.BorderSize = 0;
            btnRegressState.Click += BtnRegressState_Click;
            grpActions.Controls.Add(btnRegressState);
        }

        private void LoadTickets()
        {
            var tickets = _controller.GetAllTickets();
            dgvTickets.DataSource = tickets.Select(t => new
            {
                ID = t.Id,
                Título = t.Title,
                Estado = t.CurrentState,
                Prioridad = t.Priority.Name,
                Asignado = t.AssignedTo ?? "Sin asignar",
                Creado = t.CreatedDate.ToString("dd/MM/yyyy HH:mm")
            }).ToList();

            // Configurar colores de las columnas
            if (dgvTickets.Columns.Count > 0)
            {
                dgvTickets.Columns["ID"].Width = 50;
                dgvTickets.Columns["Título"].Width = 200;
                dgvTickets.Columns["Estado"].Width = 120;
                dgvTickets.Columns["Prioridad"].Width = 100;
                dgvTickets.Columns["Asignado"].Width = 150;
                dgvTickets.Columns["Creado"].Width = 120;
            }

            UpdateCurrentTicketDisplay();
        }

        private void UpdateCurrentTicketDisplay()
        {
            var currentTicket = _controller.GetCurrentTicket();
            if (currentTicket != null)
            {
                txtTitle.Text = currentTicket.Title ?? "";
                txtDescription.Text = currentTicket.Description ?? "";
                txtAssignedTo.Text = currentTicket.AssignedTo ?? "";
                cmbPriority.SelectedItem = currentTicket.Priority.Name.ToLower();
                lblCurrentState.Text = $"Estado: {currentTicket.CurrentState}";
                lblTicketInfo.Text = $"Creado: {currentTicket.CreatedDate:dd/MM/yyyy HH:mm} | ID: #{currentTicket.Id}";

                // Actualizar historial
                lstHistory.Items.Clear();
                foreach (var historyItem in currentTicket.History)
                {
                    lstHistory.Items.Add(historyItem);
                }

                // Configurar permisos de edición
                bool canEdit = currentTicket.CanEdit;
                txtTitle.ReadOnly = !canEdit;
                txtDescription.ReadOnly = !canEdit;
                txtAssignedTo.ReadOnly = !canEdit;
                cmbPriority.Enabled = canEdit;
                btnUpdate.Enabled = canEdit;

                // Cambiar colores según el estado
                switch (currentTicket.CurrentState)
                {
                    case "Nuevo":
                        lblCurrentState.ForeColor = Color.FromArgb(23, 162, 184);
                        break;
                    case "En Progreso":
                        lblCurrentState.ForeColor = Color.FromArgb(255, 193, 7);
                        break;
                    case "Resuelto":
                        lblCurrentState.ForeColor = Color.FromArgb(40, 167, 69);
                        break;
                    case "Cerrado":
                        lblCurrentState.ForeColor = Color.FromArgb(108, 117, 125);
                        break;
                }
            }
        }

        private void UpdateNavigationInfo()
        {
            var current = _controller.GetCurrentTicket();
            if (current != null)
            {
                var totalTickets = _controller.GetAllTickets().Count;
                var currentIndex = _controller.GetAllTickets().IndexOf(current) + 1;
                lblCurrentTicket.Text = $"Ticket {currentIndex} de {totalTickets}: #{current.Id} - {current.Title}";
            }

            btnPrevious.Enabled = _controller.HasPreviousTicket();
            btnNext.Enabled = _controller.HasNextTicket();
        }

        private void DgvTickets_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTickets.SelectedRows.Count > 0)
            {
                var selectedId = (int)dgvTickets.SelectedRows[0].Cells["ID"].Value;

                // Buscar el ticket en el iterador
                _controller.ResetIterator();
                var current = _controller.GetCurrentTicket();
                while (current != null && current.Id != selectedId)
                {
                    if (!_controller.HasNextTicket()) break;
                    current = _controller.NextTicket();
                }

                UpdateCurrentTicketDisplay();
                UpdateNavigationInfo();
            }
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            _controller.PreviousTicket();
            UpdateCurrentTicketDisplay();
            UpdateNavigationInfo();

            // Actualizar selección en el grid
            var current = _controller.GetCurrentTicket();
            if (current != null)
            {
                foreach (DataGridViewRow row in dgvTickets.Rows)
                {
                    if ((int)row.Cells["ID"].Value == current.Id)
                    {
                        row.Selected = true;
                        dgvTickets.CurrentCell = row.Cells[0];
                        break;
                    }
                }
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            _controller.NextTicket();
            UpdateCurrentTicketDisplay();
            UpdateNavigationInfo();

            // Actualizar selección en el grid
            var current = _controller.GetCurrentTicket();
            if (current != null)
            {
                foreach (DataGridViewRow row in dgvTickets.Rows)
                {
                    if ((int)row.Cells["ID"].Value == current.Id)
                    {
                        row.Selected = true;
                        dgvTickets.CurrentCell = row.Cells[0];
                        break;
                    }
                }
            }
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || cmbPriority.SelectedItem == null)
            {
                MessageBox.Show("Por favor complete al menos el título y la prioridad.",
                    "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _controller.CreateTicket(
                txtTitle.Text.Trim(),
                txtDescription.Text.Trim(),
                txtAssignedTo.Text.Trim(),
                cmbPriority.SelectedItem.ToString()
            );

            LoadTickets();
            UpdateNavigationInfo();
            ClearForm();

            MessageBox.Show("Ticket creado exitosamente.",
                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            var current = _controller.GetCurrentTicket();
            if (current != null && current.CanEdit)
            {
                if (string.IsNullOrWhiteSpace(txtTitle.Text) || cmbPriority.SelectedItem == null)
                {
                    MessageBox.Show("Por favor complete al menos el título y la prioridad.",
                        "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _controller.UpdateTicket(
                    current.Id,
                    txtTitle.Text.Trim(),
                    txtDescription.Text.Trim(),
                    txtAssignedTo.Text.Trim(),
                    cmbPriority.SelectedItem.ToString()
                );

                LoadTickets();
                UpdateCurrentTicketDisplay();

                MessageBox.Show("Ticket actualizado exitosamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se puede editar un ticket en este estado.",
                    "Operación no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtTitle.Clear();
            txtDescription.Clear();
            txtAssignedTo.Clear();
            cmbPriority.SelectedIndex = -1;
            txtTitle.Focus();
        }

        private void BtnAdvanceState_Click(object sender, EventArgs e)
        {
            var current = _controller.GetCurrentTicket();
            if (current != null)
            {
                string currentState = current.CurrentState;
                _controller.AdvanceTicketState(current.Id);

                if (current.CurrentState != currentState)
                {
                    LoadTickets();
                    UpdateCurrentTicketDisplay();
                    MessageBox.Show($"Estado cambiado de '{currentState}' a '{current.CurrentState}'.",
                        "Estado actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se puede avanzar más desde este estado.",
                        "Límite alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnRegressState_Click(object sender, EventArgs e)
        {
            var current = _controller.GetCurrentTicket();
            if (current != null)
            {
                string currentState = current.CurrentState;
                _controller.RegressTicketState(current.Id);

                if (current.CurrentState != currentState)
                {
                    LoadTickets();
                    UpdateCurrentTicketDisplay();
                    MessageBox.Show($"Estado cambiado de '{currentState}' a '{current.CurrentState}'.",
                        "Estado actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se puede retroceder más desde este estado.",
                        "Límite alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // Ajustar el tamaño del DataGridView cuando se redimensiona la ventana
            if (dgvTickets != null && panelTop != null)
            {
                dgvTickets.Size = new Size(panelTop.Width - 25, dgvTickets.Height);
            }
        }
    }
}