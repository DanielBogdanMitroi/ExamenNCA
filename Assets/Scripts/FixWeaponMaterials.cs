using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class FixWeaponMaterials : MonoBehaviour
{
    [MenuItem("Tools/Fix Weapon Materials (URP)")]
    static void FixAllWeaponMaterials()
    {
        Debug.Log("=== Iniciando conversión de materiales ===");
        
        // Buscar todos los materiales en la carpeta del pack
        string[] materialPaths = new string[]
        {
            "Assets/4K 3D Weapons Mega Pack",
        };
        
        List<Material> materials = new List<Material>();
        
        foreach (string path in materialPaths)
        {
            string[] guids = AssetDatabase.FindAssets("t:Material", new[] { path });
            
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                Material mat = AssetDatabase.LoadAssetAtPath<Material>(assetPath);
                
                if (mat != null)
                {
                    materials.Add(mat);
                }
            }
        }
        
        Debug.Log($"Encontrados {materials.Count} materiales");
        
        int fixedCount = 0; // Cambiado de 'fixed' a 'fixedCount'
        
        foreach (Material mat in materials)
        {
            // Verificar si el shader es Standard (Built-in)
            if (mat.shader.name == "Standard" || mat.shader.name.Contains("Legacy"))
            {
                Debug.Log($"Convirtiendo: {mat.name}");
                
                // Guardar texturas antes de cambiar shader
                Texture mainTex = mat.GetTexture("_MainTex");
                Texture normalMap = mat.GetTexture("_BumpMap");
                Texture metallicMap = mat.GetTexture("_MetallicGlossMap");
                Texture occlusionMap = mat.GetTexture("_OcclusionMap");
                
                // Cambiar a shader URP
                Shader urpShader = Shader.Find("Universal Render Pipeline/Lit");
                
                if (urpShader != null)
                {
                    mat.shader = urpShader;
                    
                    // Reasignar texturas
                    if (mainTex != null)
                        mat.SetTexture("_BaseMap", mainTex);
                    
                    if (normalMap != null)
                        mat.SetTexture("_BumpMap", normalMap);
                    
                    if (metallicMap != null)
                        mat.SetTexture("_MetallicGlossMap", metallicMap);
                    
                    if (occlusionMap != null)
                        mat.SetTexture("_OcclusionMap", occlusionMap);
                    
                    EditorUtility.SetDirty(mat);
                    fixedCount++;
                    
                    Debug.Log($"✓ {mat.name} convertido exitosamente");
                }
                else
                {
                    Debug.LogError("Shader URP/Lit no encontrado. ¿Tu proyecto usa URP?");
                }
            }
        }
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        
        Debug.Log($"=== Conversión completada: {fixedCount}/{materials.Count} materiales convertidos ===");
        EditorUtility.DisplayDialog("Conversión Completa", 
            $"Se han convertido {fixedCount} materiales a URP.\n\nPresiona Play para ver los cambios.", 
            "OK");
    }
}