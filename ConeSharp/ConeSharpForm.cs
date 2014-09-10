using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConeSharp
{
    public partial class ConeSharpForm : Form
    {
        readonly Stack<IStep> _steps = new Stack<IStep>();
        private readonly State _state;

        public ConeSharpForm()
        {
            _state = new State(this);
            InitializeComponent();
            SetStep(new IntroStep());
        }

        private void SetStep(IStep step)
        {
            TopMost = step.TopMost;
            _steps.Push(step);
            step.Initialize(_state);

            SetUI(step);
        }

        private void SetUI(IStep step)
        {
            label1.Text = step.Title;
            label2.Text = step.Description;
            label3.Text = step.MoreInfo;
            if (string.IsNullOrEmpty(step.ButtonText))
            {
                buttonOpt.Visible = false;
            }
            else
            {
                buttonOpt.Visible = true;
                buttonOpt.Text = step.ButtonText;
            }
            buttonBack.Enabled = _steps.Count > 1;

            labelWestGoto.Text = FormatPos(PositionId.WestGoto);
            labelWestAlign.Text = FormatPos(PositionId.WestAlign);
            labelEastGoto.Text = FormatPos(PositionId.EastGoto);
            labelEastAlign.Text = FormatPos(PositionId.EastAlign);

            labelWestOffset.Text = FormatPos2(PositionId.WestAlign, PositionId.WestGoto);
            labelEastOffset.Text = FormatPos2(PositionId.EastAlign, PositionId.EastGoto);
        }

        private string FormatPos2(PositionId align, PositionId gotoId)
        {
            var alignPos = _state.GetPosition(align);
            var gotoPos = _state.GetPosition(gotoId);
            if (alignPos == null || gotoPos == null)
                return "";

            var offset = alignPos - gotoPos;

            return string.Format("RA (deg) = {0}, Dec = {1}", Utils.FormatDegrees(offset.RA * 15), Utils.FormatDegrees(offset.Declination));
        }

        private string FormatPos(PositionId positionId)
        {
            var pos = _state.GetPosition(positionId);
            if (pos == null)
                return "";

            return string.Format("RA = {0}, Dec = {1}", Utils.FormatRA(pos.RA), Utils.FormatDegrees(pos.Declination));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var last = _steps.Peek();
            var next = last.OnNext();
            if (next == null)
                return;
            SetStep(next);
        }

        private void buttonOpt_Click(object sender, EventArgs e)
        {
            _steps.Peek().OnButton();
            SetUI(_steps.Peek());
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (_steps.Count > 1)
            {
                _steps.Pop();
                SetUI(_steps.Peek());
            }
        }
    }
}
