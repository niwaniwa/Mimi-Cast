using System;
using System.Linq;
using System.Threading.Tasks;
using MimiCast.Scripts.Adapter;
using MimiCast.Scripts.Entity;
using UnityEngine;
using System.Windows.Forms;
using SFB;

namespace MimiCast.Scripts.UseCase
{
    public class UserAvatarLoader : MonoBehaviour
    {

        [SerializeField] private string path;
        [SerializeField] private MimiAvatarConnector connector;

        private MimiAvatar _avatar;
        
        public async void Start()
        {
            var avatar = await Load(path);
            connector.ApplyAvatar(avatar);
        }

        public async Task<MimiAvatar> LoadAvatar()
        {
            var extension = new [] {
                new ExtensionFilter("vrm Files", "vrm", "VRM"),
            };
            var path = StandaloneFileBrowser.OpenFilePanel("Load VRM File", "", extension,false);

            if (path.Length <= 0) return null;

            return await Load(path[0]);

        }

        private async Task<MimiAvatar> Load(string targetpath)
        {
            var loader = new VrmLoader(targetpath);
            _avatar = await loader.LoadModel();
            
            if (_avatar == null)
            {
                return null;
            }

            return _avatar;
        } 
    }
}