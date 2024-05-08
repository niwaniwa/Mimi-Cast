using System;
using MimiCast.Scripts.Adapter;
using MimiCast.Scripts.Entity;
using UnityEngine;

namespace MimiCast.Scripts.Usecase
{
    public class UserAvatarLoader : MonoBehaviour
    {

        [SerializeField] private string path;
        
        public void Start()
        {
            var loader = new VrmLoader(path);
            var task = loader.LoadModel();
            task.Wait();
            var avater = new MimiAvatar(loader.Runtime);
        }
    }
}