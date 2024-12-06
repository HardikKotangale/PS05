using UnityEngine;


namespace Lab13_hkotanga{ 
public class DragObject : MonoBehaviour {
    private Camera mainCamera;
    private Vector3 startMousePos;
    private Vector3 CofM = new Vector3(0f, 1.5f, 0f);
    private Matrix4x4 RotMat = Matrix4x4.identity;  
    private Vector3 scaleFactors = new Vector3(2.5f, 2.5f, 2.5f);  
    private const float R = 100.0f;  
    private const float speed = 0.062f;  
    private const float scaleSpeed = 0.01f;  
    private enum InteractionMode { None, TranslateXY, TranslateXZ, Rotate, Scale }
    private InteractionMode currentMode = InteractionMode.None;

    private void Awake() {
        mainCamera = Camera.main;
    }

    public void SetInteractionMode(int mode) {
        currentMode = (InteractionMode)mode;
    }

    private void OnMouseDown() {
        startMousePos = Input.mousePosition;
    }

    private void OnMouseDrag() {
        Vector3 currentMousePosition = Input.mousePosition;

        switch (currentMode) {
            case InteractionMode.TranslateXY:
                TranslateXY(currentMousePosition - startMousePos);
                break;
            case InteractionMode.TranslateXZ:
                TranslateXZ(currentMousePosition - startMousePos);
                break;
            case InteractionMode.Rotate:
                RotateObject(currentMousePosition);
                break;
            case InteractionMode.Scale:
                ScaleObject(currentMousePosition - startMousePos);
                break;
        }

        ApplyTransformation();
        startMousePos = currentMousePosition;   
    }

    private void TranslateXY(Vector3 mouseDelta) {  
        CofM += new Vector3(mouseDelta.x * speed * 0.1f, mouseDelta.y * speed * 0.1f, 0);
    }

    private void TranslateXZ(Vector3 mouseDelta) { 
        CofM += new Vector3(mouseDelta.x * speed * 0.1f, 0, mouseDelta.y * speed * 0.1f);
    }

    private void RotateObject(Vector3 currentMousePosition) {
        float dx = -currentMousePosition.x + startMousePos.x;
        float dy = -currentMousePosition.y + startMousePos.y;
        float dr = Mathf.Sqrt(dx * dx + dy * dy);  

        if (dr < Mathf.Epsilon) return;  

        Vector3 rotationAxis = new Vector3(-dy, dx, 0).normalized;  
        float angle = Mathf.Asin(Mathf.Clamp(dr / R, -1f, 1f)) * Mathf.Rad2Deg;
        angle *= speed;
        Matrix4x4 newRotation = Matrix4x4.Rotate(Quaternion.AngleAxis(angle, rotationAxis));
        RotMat = newRotation * RotMat;
    }

    private void ScaleObject(Vector3 mouseDelta) {
    
    float scaleChangeX = mouseDelta.x * scaleSpeed;
    float scaleChangeY = mouseDelta.y * scaleSpeed;

    scaleFactors.x += scaleChangeX;
    scaleFactors.y += scaleChangeY;

    scaleFactors.x = Mathf.Max(scaleFactors.x, 0.01f);
    scaleFactors.y = Mathf.Max(scaleFactors.y, 0.01f);
    }

    private void ApplyTransformation() {
        Matrix4x4 translationMatrix = Matrix4x4.Translate(CofM);
        Matrix4x4 scalingMatrix = Matrix4x4.Scale(scaleFactors);
        Matrix4x4 modelingMatrix = translationMatrix * RotMat * scalingMatrix;

        transform.position = modelingMatrix.MultiplyPoint3x4(Vector3.zero);
        transform.rotation = modelingMatrix.rotation;
        transform.localScale = new Vector3(
            modelingMatrix.lossyScale.x,
            modelingMatrix.lossyScale.y,
            modelingMatrix.lossyScale.z
        );
    }

    public void ResetTransformations() {
        CofM = Vector3.zero;
        RotMat = Matrix4x4.identity;
        scaleFactors = new Vector3(2.5f, 2.5f, 2.5f);
        ApplyTransformation();
    }

    
    public void ToggleObjectActive() {
        gameObject.SetActive(true);
    }

    public void ToggleObjectDeactive() {
        gameObject.SetActive(false);
    }
    
}
}