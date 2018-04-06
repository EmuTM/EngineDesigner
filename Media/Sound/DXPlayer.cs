using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using SlimDX.DirectSound;
using SlimDX.Multimedia;

using System.Windows.Forms;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using System.CodeDom;
using System.Globalization;
using System.IO;

namespace EngineDesigner.Media.Sound
{
    [DesignerSerializer(typeof(DXPlayerSerializer), typeof(CodeDomSerializer))]
    public partial class DXPlayer : Component
    {
        private DirectSound directSound = new DirectSound();



        private Control owner;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public Control Owner
        {
            get { return this.owner; }

            set
            {
                this.owner = value;
                this.directSound.SetCooperativeLevel(this.owner.Handle, CooperativeLevel.Normal);
            }
        }

        private List<SoundPatch> soundsPatches = new List<SoundPatch>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public List<SoundPatch> SoundsPatches
        {
            get { return soundsPatches; }
            set { soundsPatches = value; }
        }



        public DXPlayer()
            : this(null)
        {
        }
        public DXPlayer(IContainer _iContainer)
        {
            if (_iContainer != null)
            {
                _iContainer.Add(this);
            }


            InitializeComponent();


            this.Disposed
                += new EventHandler(DXPlayer_Disposed);
        }
        private void DXPlayer_Disposed(object sender, EventArgs e)
        {
            this.Disposed
                += new EventHandler(DXPlayer_Disposed);


            if (this.directSound != null)
            {
                this.directSound.Dispose();
                this.directSound = null;
            }
        }



        public SoundPatch AddSoundPatch(Wave _wave)
        {
            SoundPatch _soundPatch = new SoundPatch(this.directSound, _wave);
            this.soundsPatches.Add(_soundPatch);

            return _soundPatch;
        }
        public SoundPatch GetSoundPatchByTag(object _tag)
        {
            foreach (SoundPatch _soundPatch in this.soundsPatches)
            {
                if (_soundPatch.Tag == _tag)
                {
                    return _soundPatch;
                }
            }

            throw new Exception("ne obstaja");
        }

    }


    /// <summary>
    /// Provides additional code serialization for DXPlayer class.
    /// </summary>
    class DXPlayerSerializer : CodeDomSerializer
    {
        public override object Serialize(IDesignerSerializationManager manager, object value)
        {
            CodeDomSerializer _codeDomSerializer = (CodeDomSerializer)manager.GetSerializer(
                typeof(DXPlayer).BaseType, typeof(CodeDomSerializer));

            object _object = _codeDomSerializer.Serialize(manager, value);
            if (_object is CodeStatementCollection)
            {
                CodeStatementCollection _codeStatementCollection = (CodeStatementCollection)_object;

                CodeExpression _targetObject = base.GetExpression(manager, value);
                if (_targetObject != null)
                {
                    CodePropertyReferenceExpression _codePropertyReferenceExpression =
                        new CodePropertyReferenceExpression(_targetObject, "Owner");
                    CodeAssignStatement _codeAssignStatement = new CodeAssignStatement(
                        _codePropertyReferenceExpression, new CodeThisReferenceExpression());

                    _codeStatementCollection.Insert(0, _codeAssignStatement);
                }
            }


            return _object;
        }

    }

}
