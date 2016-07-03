using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildCheck : MonoBehaviour {

#if DEVELOP
    const string Environment = "DEVELOP";
#elif RELEASE
    const string Environment = "Release";
#else
    const string Environment = "None";
#endif

    [SerializeField] Text envText;

    void Start(){
        envText.text = Environment;
    }

}
