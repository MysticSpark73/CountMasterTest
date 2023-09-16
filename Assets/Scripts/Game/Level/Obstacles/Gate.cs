using System;
using TMPro;
using UnityEngine;

namespace CountMasters.Game.Level.Obstacles
{
    public class Gate : Obstacle
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private GateOperation _gateOperation;
        [SerializeField] private int _gateOperationValue;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Renderer _renderer;

        private readonly string multiplication_label = "X";
        private readonly string division_label = "/";
        private readonly string addition_label = "+";
        private readonly string subtraction_label = "-";

        public override void Init(params object[] args)
        {
            SetText();
        }
        
        public override void Reset()
        {
            _collider.enabled = true;
            SetText();
        }

        public void SetColor(Color color)
        {
            _renderer.sharedMaterials[0].color = color;
        }

        private void OnTriggerEnter(Collider other)
        {
            LevelEvents.GateTrigger?.Invoke(_gateOperation, _gateOperationValue);
            _collider.enabled = false;
        }

        private void SetText()
        {
            _text.text = $"{GateOperationToString(_gateOperation)}{_gateOperationValue}";
        }

        private string GateOperationToString(GateOperation operation) => operation switch
        {
            GateOperation.Multiplication => multiplication_label,
            GateOperation.Division => division_label,
            GateOperation.Addition => addition_label,
            GateOperation.Subtraction => subtraction_label,
            _ => String.Empty
        };
    }
}