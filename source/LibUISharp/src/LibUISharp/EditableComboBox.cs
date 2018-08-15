﻿using System;
using LibUISharp.Internal;
using LibUISharp.SafeHandles;

namespace LibUISharp
{
    /// <summary>
    /// Represents a selection control with a drop-down list that can be shown or hidden by clicking the arrow on the control, and can be typed into.
    /// </summary>
    [NativeType("uiEditableCombobox")]
    public class EditableComboBox : Control
    {
        private string text;

        /// <summary>
        /// Initalizes a new instance of the <see cref="ComboBox"/> class.
        /// </summary>
        public EditableComboBox() : base()
        {
            Handle = new SafeControlHandle(NativeCalls.NewEditableCombobox());
            InitializeEvents();
        }

        /// <summary>
        /// Occurs when the <see cref="Text"/> property is changed.
        /// </summary>
        public event Action TextChanged;

        /// <summary>
        /// Gets or sets the text of this <see cref="EditableComboBox"/>.
        /// </summary>
        public virtual string Text
        {
            get
            {
                text = NativeCalls.EditableComboboxText(Handle);
                return text;
            }
            set
            {
                if (text != value)
                {
                    NativeCalls.EditableComboboxSetText(Handle, value);
                    text = value;
                }
            }
        }

        /// <summary>
        /// Adds a drop-down item to this <see cref="EditableComboBox"/>.
        /// </summary>
        /// <param name="item">The item to add to this control.</param>
        public void Add(string item) => NativeCalls.EditableComboboxAppend(Handle, item);

        /// <summary>
        /// Adds drop-down items to this <see cref="EditableComboBox"/>.
        /// </summary>
        /// <param name="items">The items to add to this control</param>
        public void Add(params string[] items)
        {
            foreach (string s in items)
            {
                Add(s);
            }
        }

        /// <summary>
        /// Called when the <see cref="TextChanged"/> event is raised.
        /// </summary>
        protected virtual void OnTextChanged() => TextChanged?.Invoke();

        /// <summary>
        /// Initializes this UI component's events.
        /// </summary>
        protected sealed override void InitializeEvents() => NativeCalls.EditableComboboxOnChanged(Handle, (box, data) => { OnTextChanged(); }, IntPtr.Zero);
    }
}