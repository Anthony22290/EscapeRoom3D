using UnityEngine;

namespace Asterion
{
    [RequireComponent(typeof(TextMesh))]
    public sealed class WorldTextDepth : MonoBehaviour
    {
        public Shader shader;
        TextMesh label;
        Material material;
        void Start()
        {
            label = GetComponent<TextMesh>();
            if (shader == null || label.font == null) return;
            label.font.RequestCharactersInTexture(label.text, label.fontSize, label.fontStyle);
            material = new Material(shader);
            material.mainTexture = label.font.material.mainTexture;
            GetComponent<MeshRenderer>().sharedMaterial = material;
            Font.textureRebuilt += Refresh;
        }
        void Refresh(Font updatedFont)
        {
            if (material != null && label != null && updatedFont == label.font)
                material.mainTexture = updatedFont.material.mainTexture;
        }
        void OnDestroy()
        {
            Font.textureRebuilt -= Refresh;
            if (material != null) Destroy(material);
        }
    }
}
