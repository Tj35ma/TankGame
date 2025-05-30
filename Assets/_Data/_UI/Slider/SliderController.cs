using UnityEngine;

public abstract class SliderController : SliderAbstract
{   
    protected void FixedUpdate()
    {
        this.UpdateSlider();
    }

    protected virtual void UpdateSlider()
    {
        this.slider.value = this.GetValue();
    }
    protected abstract float GetValue();    
}
