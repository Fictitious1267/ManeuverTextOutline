using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ManeuverTextOutline
{

    [KSPAddon(KSPAddon.Startup.Flight, true)]
    class AddOutlineToMapViewTextInFlight : AddOutlineToMapViewText
    {
    }


    [KSPAddon(KSPAddon.Startup.TrackingStation, true)]
    class AddOutlinetoMapViewInTrackingStation : AddOutlineToMapViewText
    {
    }


    abstract class AddOutlineToMapViewText : MonoBehaviour
    {
        private const float HorizontalEffect = 1.0f;
        private const float VerticalEffect = 1.0f;
        private readonly Color _color = Color.black;

        private static bool _ranFlag = false;

        private void Start()
        {
            if (!_ranFlag) AddOutline();
            _ranFlag = true;
            Destroy(gameObject);
        }


        private void AddOutline()
        {
            // Search for "Caption" text. There's only one. Use version of GetComponents that searches inactive GO like this prefab
            var text = MapView.UINodePrefab.GetComponentsInChildren<Text>(true).FirstOrDefault();

            if (text == null)
            {
                Debug.LogError("Didn't find expected Text component in UINodePrefab: can't add outline");
                return;
            }

            var outline = text.gameObject.AddComponent<Outline>();
            outline.effectColor = _color;
            outline.effectDistance = new Vector2(HorizontalEffect, VerticalEffect);
            outline.useGraphicAlpha = true;
        }
    }
}