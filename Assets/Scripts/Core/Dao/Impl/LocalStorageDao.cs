using System.IO;
using Core.FileHandler;
using Core.Utils;
using UnityEngine;

namespace Core.Dao.Impl
{
    public class LocalStorageDao<T> : IDao<T> where T: class
    {
        private readonly string _filename;

        public LocalStorageDao(string filename)
        {
            _filename = filename;
        }

        public void Save(T vo)
        {
            var json = JsonUtility.ToJson(vo);
            var serialized = json.Base64Encode();
            var path = GetPath();
            FileHandling.CreateDirectoryIfDoesntExistAndWriteAllText(path, serialized);
        }

        public T Load()
        {
            var path = GetPath();
            if (!File.Exists(path))
                return null;
            var json = File.ReadAllText(path).Base64Decode();
            return JsonUtility.FromJson<T>(json);
        }

        public void Remove()
        {
            var path = GetPath();
            if (File.Exists(path))
            {
                FileHandling.DeleteIfExists(path);
            }
        }

        private string GetPath()
        {
            return Application.persistentDataPath + _filename;
        }
    }
}