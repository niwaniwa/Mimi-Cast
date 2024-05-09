using System;
using System.Threading.Tasks;
using MimiCast.Scripts.Entity;
using UniGLTF;
using UniVRM10;

namespace MimiCast.Scripts.Adapter
{
    public class VrmLoader
    {

        private string _path;
        
        public VrmLoader(string path)
        {
            _path = path;
        }

        public async Task<MimiAvatar> LoadModel()
        {
            var instance = await Vrm10.LoadPathAsync(_path,
                true,
                ControlRigGenerationOption.Generate,
                true);
            if (instance == null)
            {
                return null;
            }
            return new MimiAvatar(instance);
        }

        public ControlRigGenerationOption ControlRigGenerationOption { get; set; }
    }
}