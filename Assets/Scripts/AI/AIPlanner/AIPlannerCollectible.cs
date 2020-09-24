using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Planner.DomainLanguage.TraitBased;
using UnityEngine.AI.Planner.DomainLanguage.TraitBased;
using Generated.AI.Planner.StateRepresentation;

public class AIPlannerCollectible : MonoBehaviour
{
    TraitComponent traitComponent;
    AmmoBox ammoBox;
    HealthBox healthBox;

    void Start()
    {
        ammoBox = GetComponent<AmmoBox>();
        healthBox = GetComponent<HealthBox>();

        traitComponent = GetComponent<TraitComponent>();

        UpdateParams();
    }

    void Update()
    {
        UpdateParams();
    }

    void UpdateParams()
    {
        ITraitData collectible = traitComponent.GetTraitData<Collectible>();
        if (ammoBox != null)
            collectible.SetValue("Active", ammoBox.IsActive);
        else if (healthBox != null)
            collectible.SetValue("Active", healthBox.IsActive);
    }
}
