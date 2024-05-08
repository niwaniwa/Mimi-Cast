using System;
using System.Threading.Tasks;
using UniGLTF;
using UniVRM10;

namespace MimiCast.Scripts.Adapter
{
    public class VrmLoader
    {

        private string _path;

        private Vrm10Instance _vrmController;

        public Vrm10Runtime Runtime => _vrmController.Runtime;

        public VrmLoader(string path)
        {
            this._path = path;
        }

        public async Task LoadModel()
        {
            _vrmController = await Vrm10.LoadPathAsync(_path);
        }
    }
}