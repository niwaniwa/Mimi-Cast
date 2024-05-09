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
            var instance = await Vrm10.LoadPathAsync(_path);
            if (instance == null)
            {
                return null;
            }
            var modelData = instance.GetComponent<RuntimeGltfInstance>();
            modelData.ShowMeshes();
            modelData.EnableUpdateWhenOffscreen();
            return new MimiAvatar(modelData);
        }
    }
}