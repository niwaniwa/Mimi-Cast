using MimiCast.Scripts.Entity;
using UniGLTF;
using UnityEngine;
using UniVRM10;

namespace MimiCast.Scripts.Utility
{
    public class MimiAvatarLoadTester : MonoBehaviour
    {
        public string path;

        private MimiAvatar _avatar;

        public async void Load()
        {
            if (_avatar != null)
            {
                Dispose();
            }
            
            var instance = await Vrm10.LoadPathAsync(path);
            if (instance == null)
            {
                return;
            }
            var modelData = instance.GetComponent<RuntimeGltfInstance>();
            modelData.ShowMeshes();
            modelData.EnableUpdateWhenOffscreen();
            _avatar = new MimiAvatar(instance);
            return;
        }

        public void Dispose()
        {
            GameObject.Destroy(_avatar.ModelData.gameObject);
        }
        
    }
}