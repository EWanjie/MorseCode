using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage _Image;
    [SerializeField] private float _SpeedVertical;
    [SerializeField] private float _SpeedHorizontal;

    // Update is called once per frame
    void Update()
    {
        _Image.uvRect = new Rect(_Image.uvRect.position +  new Vector2(_SpeedHorizontal, _SpeedVertical) * Time.deltaTime, _Image.uvRect.size); 
    }
}
