using System;
using MimiCast.Scripts.Adapter;
using MimiCast.Scripts.Entity;
using UnityEngine;

namespace MimiCast.Scripts.UseCase
{
    public class UserAvatarLoader : MonoBehaviour
    {

        [SerializeField] private string path;

        private MimiAvatar _avatar;
        
        public async void Start()
        {

            var loader = new VrmLoader(path);
            _avatar = await loader.LoadModel();
            
            if (_avatar == null)
            {
                return;
            }
        }
    }
}