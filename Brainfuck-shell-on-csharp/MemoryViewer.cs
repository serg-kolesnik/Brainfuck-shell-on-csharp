using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace Brainfuck_interpretator
{


    namespace BrainfuckInterpreter
    {
        public class MemoryViewer : UserControl
        {
            // Типы отображения ячеек
            private Dictionary<string, Func<byte, string>> cellRenderers;
            private string currentRendererLabel = "hex";

            // Элементы управления
            private ComboBox rendererSelector;
            private FlowLayoutPanel memoryPanel;

            // Данные
            private byte[] memory;
            private int memoryPointer;

            public MemoryViewer()
            {
                InitializeComponent();
                SetupRenderers();
            }

            private void InitializeComponent()
            {
                // Настройка основного контрола
                this.Size = new Size(600, 400);
                this.Dock = DockStyle.Fill;
                this.BackColor = Color.White;
                this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);

                // Создаем селектор типа отображения
                rendererSelector = new ComboBox
                {
                    Location = new Point(10, 10),
                    Size = new Size(120, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                rendererSelector.SelectedIndexChanged += RendererSelector_SelectedIndexChanged;

                // Создаем панель для ячеек памяти
                memoryPanel = new FlowLayoutPanel
                {
                    Location = new Point(10, 45),
                    Size = new Size(580, 340),
                    AutoScroll = true,
                    WrapContents = true,
                    FlowDirection = FlowDirection.LeftToRight,
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle
                };

                // Добавляем элементы на контрол
                this.Controls.Add(rendererSelector);
                this.Controls.Add(memoryPanel);
            }

            private void SetupRenderers()
            {
                cellRenderers = new Dictionary<string, Func<byte, string>>
                {
                    ["hex"] = value => value.ToString("X2"),
                    ["dec"] = value => value.ToString(),
                    ["ascii"] = value =>
                    {
                        char c = (char)value;
                        // Заменяем непечатаемые символы
                        if (value < 0x20 || value == 0x7F || (value >= 0x80 && value <= 0x9F) || value == 0xAD)
                        {
                            return $"\\u{(int)value:X4}";
                        }
                        return c.ToString();
                    }
                };

                // Заполняем комбобокс
                rendererSelector.Items.Clear();
                foreach (string key in cellRenderers.Keys)
                {
                    rendererSelector.Items.Add(key);
                }
                rendererSelector.SelectedItem = "hex";
            }

            private void RendererSelector_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (rendererSelector.SelectedItem != null)
                {
                    currentRendererLabel = rendererSelector.SelectedItem.ToString();
                    RefreshMemoryView();
                }
            }

            public void UpdateMemory(byte[] newMemory, int newPointer)
            {
                memory = newMemory;
                memoryPointer = newPointer;
                RefreshMemoryView();
            }

            private void RefreshMemoryView()
            {
                if (memory == null) return;

                // Очищаем панель
                memoryPanel.SuspendLayout();
                memoryPanel.Controls.Clear();

                // Показываем только первые 256 ячеек для производительности
                int cellsToShow = Math.Min(memory.Length, 256);

                for (int address = 0; address < cellsToShow; address++)
                {
                    byte value = memory[address];
                    bool isActive = address == memoryPointer;

                    // Создаем ячейку памяти
                    Control cell = CreateMemoryCell(address, value, isActive);
                    memoryPanel.Controls.Add(cell);
                }

                memoryPanel.ResumeLayout();
            }

            private Control CreateMemoryCell(int address, byte value, bool isActive)
            {
                Panel panel = new Panel
                {
                    Size = new Size(70, 60),
                    Margin = new Padding(2),
                    BackColor = isActive ? Color.LightBlue : Color.WhiteSmoke,
                    BorderStyle = BorderStyle.FixedSingle
                };

                // Адрес ячейки
                Label addressLabel = new Label
                {
                    Text = $"0x{address:X4}",
                    Location = new Point(3, 3),
                    Font = new Font("Consolas", 7, FontStyle.Regular),
                    ForeColor = Color.Gray,
                    AutoSize = true
                };

                // Значение ячейки
                Label valueLabel = new Label
                {
                    Text = cellRenderers[currentRendererLabel](value),
                    Location = new Point(3, 18),
                    Font = new Font("Consolas", 10, FontStyle.Bold),
                    AutoSize = true
                };

                // ASCII символ (для удобства)
                Label asciiLabel = new Label
                {
                    Text = GetAsciiChar(value),
                    Location = new Point(3, 38),
                    Font = new Font("Consolas", 8, FontStyle.Regular),
                    ForeColor = Color.DarkGreen,
                    AutoSize = true
                };

                // Подсказка со всеми форматами
                string tooltipText = string.Join(", ",
                    cellRenderers.Select(kvp => $"{kvp.Key}: {kvp.Value(value)}"));

                ToolTip tooltip = new ToolTip();
                tooltip.SetToolTip(panel, tooltipText);
                tooltip.SetToolTip(addressLabel, tooltipText);
                tooltip.SetToolTip(valueLabel, tooltipText);
                tooltip.SetToolTip(asciiLabel, tooltipText);

                panel.Controls.Add(addressLabel);
                panel.Controls.Add(valueLabel);
                panel.Controls.Add(asciiLabel);

                return panel;
            }

            private string GetAsciiChar(byte value)
            {
                if (value >= 32 && value <= 126)
                {
                    return $"'{((char)value)}'";
                }
                return "''";
            }

            // Метод для очистки памяти
            public void ClearMemory()
            {
                if (memory != null)
                {
                    Array.Clear(memory, 0, memory.Length);
                    RefreshMemoryView();
                }
            }
        }
    }
}

