using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class BaseCharacter : MonoBehaviour
{
    
    public SkeletonGraphic SkeletonGraphicCharacterBase;
    string[] skinsColor = {"Skin_Color/Base", "Skin_Color/Skin_Color_2", "Skin_Color/Skin_Color_3", "Skin_Color/Skin_Color_4", "Skin_Color/Skin_Color_5"};
    string[] skinsBody = {"Fashion/Body/Body_2", "Fashion/Body/Body_3", "Fashion/Body/Body_4", "Fashion/Body/Body_5"};
    string[] skinsHead = {"Fashion/Head/Head_2", "Fashion/Head/Head_3", "Fashion/Head/Head_4", "Fashion/Head/Head_5"};
    string[] skinFoot = {"Fashion/Foot/foot_2", "Fashion/foot/Foot_3", "Fashion/Foot/foot_4", "Fashion/Foot/foot_5"};
    string[] skinFaceCheeks ={"Face/Cheeks/Cheeks_1", "Face/Cheeks/Cheeks_2", "Face/Cheeks/Cheeks_3", "Face/Cheeks/Cheeks_4", "Face/Cheeks/Cheeks_5", "Face/Cheeks/Cheeks_6"};
    string[] skinFaceEyebrow = {"Face/Eyebrow/Eyebrow_1", "Face/Eyebrow/Eyebrow_2", "Face/Eyebrow/Eyebrow_3", "Face/Eyebrow/Eyebrow_4", "Face/Eyebrow/Eyebrow_5", "Face/Eyebrow/Eyebrow_6"};
    string[] skinFaceEye = {"Face/Eyes@/Eyes_1", "Face/Eyes@/Eyes_2", "Face/Eyes@/Eyes_3", "Face/Eyes@/Eyes_4", "Face/Eyes@/Eyes_5", "Face/Eyes@/Eyes_6"};
    string[] skinFaceMouth = {"Face/Mouth/Mouth_1", "Face/Mouth/Mouth_2", "Face/Mouth/Mouth_3", "Face/Mouth/Mouth_4", "Face/Mouth/Mouth_5", "Face/Mouth/Mouth_6"};
    string[] skinFaceNose = {"Face/Nose/Nose_1", "Face/Nose/Nose_2", "Face/Nose/Nose_3"};
    string[] skinEmoji = {"Emotion/Emo_1", "Emotion/Emo_2", "Emotion/Emo_3", "Emotion/Emo_4", "Emotion/Emo_5"};


    List<string> skinNames = new List<string>();
    [SerializeField] private int skinColorIndex;
    [SerializeField] private int skinBodyIndex;
    [SerializeField] private int skinHeadIndex;
    [SerializeField] private int skinFootIndex;
    [SerializeField] private int skinCheeksIndex;
    [SerializeField] private int skinEyebrowIndex;
    [SerializeField] private int skinEyeIndex;
    [SerializeField] private int skinMouthIndex;
    [SerializeField] private int skinNoseIndex;
    [SerializeField] private int skinEmojiIndex;

    void Start()
    {
        InitCharacter();
    }

    private void OnValidate()
    {
     // InitCharacter();
    }
    

    void InitCharacter()
    {
         skinNames.Clear();
        if (skinColorIndex >= 0 && skinColorIndex < skinsColor.Length)
            skinNames.Add(skinsColor[skinColorIndex]);
        if (skinBodyIndex >= 0 && skinBodyIndex < skinsBody.Length)
            skinNames.Add(skinsBody[skinBodyIndex]);
        if (skinHeadIndex >= 0 && skinHeadIndex < skinsHead.Length)
            skinNames.Add(skinsHead[skinHeadIndex]);
        if (skinFootIndex >= 0 && skinFootIndex < skinFoot.Length)
            skinNames.Add(skinFoot[skinFootIndex]);
        if (skinCheeksIndex >= 0 && skinCheeksIndex < skinFaceCheeks.Length)
            skinNames.Add(skinFaceCheeks[skinCheeksIndex]);
        if (skinEyebrowIndex >= 0 && skinEyebrowIndex < skinFaceEyebrow.Length)
            skinNames.Add(skinFaceEyebrow[skinEyebrowIndex]);
        if (skinEyeIndex >= 0 && skinEyeIndex < skinFaceEye.Length)
            skinNames.Add(skinFaceEye[skinEyeIndex]);
        if (skinMouthIndex >= 0 && skinMouthIndex < skinFaceMouth.Length)
            skinNames.Add(skinFaceMouth[skinMouthIndex]);
        if (skinNoseIndex >= 0 && skinNoseIndex < skinFaceNose.Length)
            skinNames.Add(skinFaceNose[skinNoseIndex]);
        if (skinEmojiIndex >= 0 && skinEmojiIndex < skinEmoji.Length)
            skinNames.Add(skinEmoji[skinEmojiIndex]);

        SetArraySkins(skinNames);
    }

    public void SetArraySkins(List<string> skinNames)
    {
        if (SkeletonGraphicCharacterBase != null && skinNames != null && skinNames.Count > 0)
        {
            Skin newSkin = new Skin("custom-skin");
            foreach (string skinName in skinNames)
            {
                Skin skin = SkeletonGraphicCharacterBase.Skeleton.Data.FindSkin(skinName);
                if (skin != null)
                {
                    newSkin.AddSkin(skin);
                }
            }
            SkeletonGraphicCharacterBase.Skeleton.SetSkin(newSkin);
            SkeletonGraphicCharacterBase.Skeleton.SetSlotsToSetupPose();
            SkeletonGraphicCharacterBase.AnimationState.Apply(SkeletonGraphicCharacterBase.Skeleton);
        }
    }
    

}
